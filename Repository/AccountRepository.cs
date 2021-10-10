using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BookStore.API.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _usermManager;
        private readonly SignInManager<ApplicationUser> _SigininManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> SigninManager,
        IConfiguration Configuration)
        {
            _usermManager=userManager;
            _SigininManager = SigninManager;
            _configuration = Configuration;
        }



        public async Task<IdentityResult> SignUpasync(SignUpModel signupmodel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signupmodel.FirstName,
                LastName = signupmodel.LastName,
                Email = signupmodel.Email,
                UserName = signupmodel.Email
            };
            return await _usermManager.CreateAsync(user,signupmodel.Password);
        }

        public async Task<string> LoginAsync(SigninModel SignInmodel)
        {
            var result =
                await _SigininManager.PasswordSignInAsync(SignInmodel.Email, SignInmodel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var authclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, SignInmodel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authsigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));



            var token=new JwtSecurityToken(
                issuer :_configuration["JWT:ValidIssuer"],
                audience :_configuration["JWT:ValidIssuer"],
                expires : DateTime.Now.AddDays(1),
                claims:  authclaims,
                signingCredentials: new SigningCredentials(authsigninKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
   
}
