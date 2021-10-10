using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.API.Models;

namespace BookStore.API.BookRepository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBookAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookModel bookmodel);
        Task UpdateBookAsync(int BookId, BookModel bookmodel);
        Task DeleteBookAsync(int BookId);
    }

}
