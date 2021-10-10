using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _Context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context,IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBookAsync()
        {
            var records =await _Context.Books.ToListAsync();

            return  _mapper.Map<List<BookModel>>(records);
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var book =await _Context.Books.FindAsync(id);

            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBookAsync(BookModel bookmodel)
        {
            var book = new Book()
            {
                Title = bookmodel.Title,
                Description = bookmodel.Description
            };
            _Context.Books.Add(book);
            await _Context.SaveChangesAsync();  

            return book.Id; 
        }



        public async Task UpdateBookAsync(int BookId, BookModel bookmodel)
        {
            var book = await _Context.Books.FindAsync(BookId);
            if (book != null)
            {
                book.Title=bookmodel.Title;  
                book.Description=bookmodel.Description;
                await _Context.SaveChangesAsync();
            }
        }


        public async Task DeleteBookAsync(int BookId)
        {
            var book = new Book()
            {
                Id = BookId
            };
            _Context.Books.Remove(book);
            await _Context.SaveChangesAsync();
        }

    }
}
