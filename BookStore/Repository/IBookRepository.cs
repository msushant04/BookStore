using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddBook(BookModel bookModel);
        Task<List<BookModel>> AllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<BookModel> SearchBook(string Name, string Auther);
    }
}