using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20_Authorize_Filter.Controllers
{

    // We can also use [Authorize] filter at a controller level
    // [Authorize]
    public class HomeController : Controller
    {
        // GET: Home

        // AllowAnonymous filters allows anyone to access the action method ( overrides [Authorize] filter of Controller, Global level

        // [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        
        // Using Authorize filter (To access this view, the user must be authorized)
        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }
    }
}