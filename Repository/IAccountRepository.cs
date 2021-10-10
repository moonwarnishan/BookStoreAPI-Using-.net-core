using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.API.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpasync(SignUpModel signupmodel);
        Task<string> LoginAsync(SigninModel SignInmodel);
    }
}
