using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2_Controller.Controllers
{
    public class TestController : Controller
    {
        // GET: Test

        // Default action method named Index that invokes corresponding index view (/Views/Test/Index.cshtml) corresponds to localhost/Test/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}