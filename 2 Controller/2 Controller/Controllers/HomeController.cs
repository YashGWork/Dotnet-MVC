using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2_Controller.Controllers
{
    public class HomeController : Controller
    {
        // This is the by default start controller which can be changed in App_Start -> RouteConfig.cs

        // Default action method named Index that invokes corresponding index view (/Views/Home/Index.cshtml) corresponds to localhost/Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // New action method that returns string instead of a view corresponds to localhost/Home/ReturnString

        public string ReturnString()
        {
            return "This is ReturnString Action method";
        }

        public ActionResult AboutUs()
        {

            return View();
        }

        // This is the action method that takes optional integer argument and returns it 

        public int Id(int id)
        {
            return id;
        }
    }
}