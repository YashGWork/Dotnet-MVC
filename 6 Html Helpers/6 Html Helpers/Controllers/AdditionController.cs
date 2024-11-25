using _6_Html_Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6_Html_Helpers.Controllers
{
    public class AdditionController : Controller
    {
        // GET: Addition
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Sum s)
        {
            ViewBag.sum = s.num1 + s.num2;
            return View();
        }


    }
}