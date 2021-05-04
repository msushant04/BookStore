using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [Route("~/")]
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
        //[Route("about-us/{id?}/{name?}")]
       // [HttpGet("about-us",Name ="about-us",Order =1)]
        public ViewResult AboutUs()//int? id,string name)
        {
            return View();
        }
        //[Route("contact-us",Name ="contact-us")]
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
