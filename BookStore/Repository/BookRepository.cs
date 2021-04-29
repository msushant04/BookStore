using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddBook(BookModel bookModel)
        {
            var newBook = new Books()
            {
                Auther = bookModel.Auther,
                Category = bookModel.Category,
                LanguageId = bookModel.LanguageId,
                Pages = bookModel.Pages.HasValue ? bookModel.Pages.Value : 0,
                Title = bookModel.Title,
                Price = bookModel.Price.HasValue ? bookModel.Price.Value : 0,
                Description = bookModel.Description,
                CreatedOn = DateTime.UtcNow
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> AllBooks()
        {
            var bookModel = new List<BookModel>();
            var AllBooks = await _context.Books.ToListAsync();
            var temp = (from a in _context.Books
                        join b in _context.Language
                        on a.LanguageId equals b.Id
                        select new BookModel
                        {
                            Id = a.Id,
                            Auther = a.Auther,
                            Category = a.Category,
                            LanguageId = a.LanguageId,
                            Language = b.Text,
                            Pages = a.Pages,
                            Title = a.Title,
                            Price = a.Price,
                            Description = a.Description,
                            Path=a.Path                            
                        }).ToList();

            #region Commented Code
            //if (AllBooks?.Any() == true)
            //{
            //    foreach (var book in AllBooks)
            //    {
            //        bookModel.Add(new BookModel()
            //        {
            //            Id = book.Id,
            //            Auther = book.Auther,
            //            Category = book.Category,
            //            LanguageId = book.LanguageId,
            //            Language = book.Language.Text,
            //            Pages = book.Pages,
            //            Title = book.Title,
            //            Price = book.Price,
            //            Description = book.Description,
            //            Path = book.Path == null ? "/images/book2.jfif" : book.Path
            //        });
            //    }
            //}
            #endregion
            return temp;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var temp = (from a in _context.Books
                        join b in _context.Language
                        on a.LanguageId equals b.Id
                        where a.Id == id
                        select new BookModel
                        {
                            Id = a.Id,
                            Auther = a.Auther,
                            Category = a.Category,
                            LanguageId = a.LanguageId,
                            Language = b.Text,
                            Pages = a.Pages,
                            Title = a.Title,
                            Price = a.Price,
                            Description = a.Description,
                            Path=a.Path
                        }).FirstOrDefault();

            #region Commented Code
            //if (book != null)
            //{
            //    var bookData = new BookModel()
            //    {
            //        Id = book.Id,
            //        Auther = book.Auther,
            //        Category = book.Category,
            //        LanguageId = book.LanguageId,
            //        Language=book.Language.Text,
            //        Pages = book.Pages,
            //        Title = book.Title,
            //        Price = book.Price,
            //        Description = book.Description
            //    };
            //    return bookData;
            //}
            #endregion
            return temp;
        }
        public List<BookModel> SearchBook(string Name, string Auther)
        {
            return null; 
        }
    }
}
