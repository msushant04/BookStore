using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            #region ExpondoOject Viewbag
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Sushant";
            //ViewBag.Data = data;
            #endregion
            return View();
        }
        public ViewResult AboutUs()
        {
            return View();
        }
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
