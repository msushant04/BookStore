using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        public readonly BookRepository _bookRepository = null;
        public readonly LanguageRepository _languageRepository = null;
        public readonly IWebHostEnvironment _webHostEnvironment = null;
        public BookController(BookRepository bookRepository, 
            LanguageRepository languageRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "images/";
                    bookModel.Path = await UploadImage(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GallaryFiles != null)
                {
                    var gallaryData = new List<GallaryModel>();
                    foreach (var file in bookModel.GallaryFiles)
                    {
                        string folder = "images/Gallary/";
                        gallaryData.Add(new GallaryModel()
                        {
                            Name = file.FileName,
                            Path = await UploadImage(folder, file)
                        });
                    }
                    bookModel.Gallary = gallaryData;
                }
                if (bookModel.BookPdf != null)
                {
                    string folder = "images/PDF/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddBook), new { isSuccess = true, BookId = id });
                }
            }

            ViewBag.Languages = new SelectList( await _languageRepository.Languages(),"Id", "Text");
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
    }
}
