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
        public async Task<ViewResult> AddBook(bool isSuccess = false, int BookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = BookId;
            var data = new BookModel()
            {
                //LanguageId = 1
            };

            #region By using SelectList
            //ViewBag.ddlList = new SelectList(GetLanguage(), "Id", "Text"); //1
            #endregion

            #region By using SelectListItem
            //ViewBag.ddlList = GetLanguage().Select(x => new SelectListItem() 
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.Text
            //}); //2
            #endregion

            #region By using SelectListGroup
            //var group1 = new SelectListGroup() { Name = "India" };
            //var group2 = new SelectListGroup() { Name = "China" };
            //var group3 = new SelectListGroup() { Name = "Pakistan" };
            //ViewBag.ddlList = new List<SelectListItem>()
            //{
            //     new SelectListItem(){ Text="English", Value="1", Group=group1 },
            //     new SelectListItem(){ Text="Hindi", Value="2", Group=group1 },
            //     new SelectListItem(){ Text="Marathi", Value="3", Group=group1 },
            //     new SelectListItem(){ Text="Urdu", Value="4", Group=group3 },
            //     new SelectListItem(){ Text="Hindi", Value="5", Group=group3 },
            //     new SelectListItem(){ Text="Chinese", Value="6", Group=group2 }
            //};
            #endregion

            #region By Using Multiselect Without Group
            //ViewBag.ddlList = new List<SelectListItem>()
            //{
            //     new SelectListItem(){ Text="English", Value="1" },
            //     new SelectListItem(){ Text="Hindi", Value="2" },
            //     new SelectListItem(){ Text="Marathi", Value="3" },
            //     new SelectListItem(){ Text="Urdu", Value="4" },
            //     new SelectListItem(){ Text="Hindi", Value="5"},
            //     new SelectListItem(){ Text="Chinese", Value="6" }
            //};
            #endregion
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

            #region By using SelectList
            //ViewBag.ddlList = new SelectList(GetLanguage(), "Id", "Text"); //1
            #endregion

            #region By using SelectListItem
            //ViewBag.ddlList = GetLanguage().Select(x => new SelectListItem() 
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.Text
            //}); //2
            #endregion

            #region By using SelectListGroup
            //var group1 = new SelectListGroup() { Name = "India" , Disabled=true};
            //var group2 = new SelectListGroup() { Name = "China" };
            //var group3 = new SelectListGroup() { Name = "Pakistan", Disabled=true };
            //ViewBag.ddlList = new List<SelectListItem>()
            //{
            //     new SelectListItem(){ Text="English", Value="1", Group=group1 },
            //     new SelectListItem(){ Text="Hindi", Value="2", Group=group1 },
            //     new SelectListItem(){ Text="Marathi", Value="3", Group=group1 },
            //     new SelectListItem(){ Text="Urdu", Value="4", Group=group3 },
            //     new SelectListItem(){ Text="Hindi", Value="5", Group=group3 },
            //     new SelectListItem(){ Text="Chinese", Value="6", Group=group2 }
            //};
            #endregion

            #region By Using Multiselect Without Group
            //ViewBag.ddlList = new List<SelectListItem>()
            //{
            //     new SelectListItem(){ Text="English", Value="1" },
            //     new SelectListItem(){ Text="Hindi", Value="2" },
            //     new SelectListItem(){ Text="Marathi", Value="3" },
            //     new SelectListItem(){ Text="Urdu", Value="4" },
            //     new SelectListItem(){ Text="Hindi", Value="5"},
            //     new SelectListItem(){ Text="Chinese", Value="6" }
            //};
            #endregion

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
