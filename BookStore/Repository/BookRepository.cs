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
                Language = bookModel.Language,
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
            if (AllBooks?.Any() == true)
            {
                foreach (var book in AllBooks)
                {
                    bookModel.Add(new BookModel()
                    {
                        Id = book.Id,
                        Auther = book.Auther,
                        Category = book.Category,
                        Language = book.Language,
                        Pages = book.Pages,
                        Title = book.Title,
                        Price = book.Price,
                        Description = book.Description,
                        Path = book.Path == null ? "/images/book2.jfif" : book.Path
                    });
                }
            }
            return bookModel;
            //return DataSource();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookData = new BookModel()
                {
                    Id = book.Id,
                    Auther = book.Auther,
                    Category = book.Category,
                    Language = book.Language,
                    Pages = book.Pages,
                    Title = book.Title,
                    Price = book.Price,
                    Description = book.Description
                };
                return bookData;
            }
            return null;
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string Name, string Auther)
        {
            return DataSource().Where(x => x.Title.Contains(Name) && x.Auther.Contains(Auther)).ToList();
        }
        public List<BookModel> DataSource()
        {
            List<BookModel> lst = new List<BookModel>()
            {
                 new BookModel(){ Id=1, Title="C#", Auther="Robin", Price=2000,Description="C# is a object oriented programming language.",Path="/images/book1.jfif",Category="Programming",Pages=1000,Language="English"},
                 new BookModel(){ Id=2, Title="Asp.Net MVC", Auther="Smith", Price=5000,Description="ASP.NET MVC is a web application framework developed by Microsoft that implements the model–view–controller pattern.",Path="/images/book2.jfif",Category="Programming",Pages=2000,Language="English"},
                 new BookModel(){ Id=3, Title="Asp.Net Core", Auther="Joe", Price=7000,Description="ASP.NET Core is a free and open-source web framework and successor to ASP.NET, developed by Microsoft.",Path="/images/book3.jfif",Category="Framework",Pages=3000,Language="English"},
                 new BookModel(){ Id=4, Title="SQL Server", Auther="Glenn", Price=1000,Description="Microsoft SQL Server is a relational database management system developed by Microsoft.",Path="/images/book4.jfif",Category="Database",Pages=4000,Language="English"},
                 new BookModel(){ Id=5, Title="jQuery", Auther="Shai", Price=1000,Description="jQuery is client side library, Developed using javascript language.",Path="/images/book5.jfif",Category="Scripting",Pages=5000,Language="English"}
            };
            return lst;
        }
    }
}
