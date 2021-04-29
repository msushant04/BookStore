using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        public readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            //return View(_bookRepository.AllBooks());
            var data = await _bookRepository.AllBooks();
            return View(data);
        }

        [Route("book-details/{id}", Name = "Bookdetails")]
        public async Task<ViewResult> GetBook(int id)
        {
            #region Dynamic View ex
            //dynamic data = new ExpandoObject();
            //data.Book= bookRepository.GetBookById(id);
            //data.CoverColor = "Red";
            #endregion

            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBook(string Name, string auther)
        {
            return _bookRepository.SearchBook(Name, auther);
        }
        public ViewResult AddBook(bool isSuccess = false, int BookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = BookId;
            var data = new BookModel()
            {
                Language = "1"
            };
            ViewBag.ddlList = new SelectList(GetLanguage(), "Id", "Text");
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddBook), new { isSuccess = true, BookId = id });
                }
            }
            ViewBag.ddlList = new SelectList(GetLanguage(), "Id", "Text");
            return View();
        }
        private List<LanguageModel> GetLanguage()
        {
            var lst = new List<LanguageModel>()
            {
                 new LanguageModel(){ Id=1, Text="English"},
                 new LanguageModel(){ Id=2,Text="Hindi"},
                 new LanguageModel(){ Id=3,Text="Marathi"}
            };
            return lst;
        }
    }
}
