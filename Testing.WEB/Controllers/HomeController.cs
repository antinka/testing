using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testing.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Question1(string mountain)
        {
            if(mountain=="Himalayas")
             return PartialView("RightAnswer");
            else
             return PartialView("WrongAnswer");
        }
        [HttpPost]
        public ActionResult Question2(string people)
        {
            if (people == "7,5")
                return PartialView("RightAnswer");
            else
                return PartialView("WrongAnswer");
        }
        [HttpPost]
        public ActionResult Question3(string lake)
        {
            if (lake == "Caspian sea")
                return PartialView("RightAnswer");
            else
                return PartialView("WrongAnswer");
        }
        [HttpPost]
        public ActionResult Question4(string country)
        {
            if (country == "Vatican")
                return PartialView("RightAnswer");
            else
                return PartialView("WrongAnswer");
        }
    }
}