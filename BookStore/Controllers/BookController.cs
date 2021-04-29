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
        public readonly LanguageRepository _languageRepository = null;
        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.AllBooks();
            return View(data);
        }

        [Route("book-details/{id}", Name = "Bookdetails")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBook(string Name, string auther)
        {
            return _bookRepository.SearchBook(Name, auther);
        }
        public async Task<ViewResult> AddBook(bool isSuccess = false, int BookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = BookId;
            var data = new BookModel()
            {
                //LanguageId = 1
            };

            var lang = await _languageRepository.Languages();
            ViewBag.Languages = new SelectList(lang, "Id", "Text");
           
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

            ViewBag.Languages = new SelectList( await _languageRepository.Languages(),"Id", "Text");
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
