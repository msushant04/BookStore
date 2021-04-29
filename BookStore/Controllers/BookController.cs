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
                Language = "English"
            };
            ViewBag.ddlList = new SelectList(new List<string>() { "English", "Marathi", "Hindi", "Telagu" });
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
            ModelState.AddModelError("", "Custom Model Error");
            ViewBag.ddlList = new List<string>() { "English", "Marathi", "Hindi", "Telagu" };
            return View();
        }
    }
}
