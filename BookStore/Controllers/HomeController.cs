using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using BookStore.Models;
using Microsoft.Extensions.Options;
using BookStore.Repository;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfig _newbookalertconfig;
        private readonly IMessageRepository _messageRepository;
        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newbookalertconfig, IMessageRepository messageRepository)
        {
            _newbookalertconfig = newbookalertconfig.Value;
            _messageRepository = messageRepository;
        }
        [Route("~/")]
        public IActionResult Index()
        {
            #region ExpondoOject Viewbag
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Sushant";
            //ViewBag.Data = data;
            #endregion

            #region #1 read configuration of appsettings.json
            //var result = configuration["AppName"];
            //var a = configuration["Obj:task1"];
            //var b = configuration["Obj:task2"];
            //var c = configuration["Obj:task3:task3.1"];
            #endregion
            #region #2, #3 GetValue and section
            //bool isTrue = configuration.GetValue<bool>("DisplayNewBookAlert");
            //var istrue = configuration["DisplayNewBookAlert"];
            #endregion
            #region #4 read configuration using Bind method ##Easy way #85
            //var newbook = new NewBookAlertConfig();
            //configuration.Bind("NewBookAlert", newbook);
            //var isAlert = newbook.DisplayNewBookAlert;
            #endregion
            #region IOptions read configuration
            var data = _newbookalertconfig.DisplayNewBookAlert;
            #endregion
            #region #87
            var dt = _messageRepository.GetName();
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
