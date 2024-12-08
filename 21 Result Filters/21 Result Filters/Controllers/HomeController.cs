using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _21_Result_Filters.Models;

// Necessary namespace for OutputCache location

using System.Web.UI;

namespace _21_Result_Filters.Controllers
{
    public class HomeController : Controller
    {
        // Creating a db context class object in order to interact with the db

        PersonDBEntities db = new PersonDBEntities();

        // GET: Home

        // The result of the action method will be stored in OutputCache for 30 seconds, and the same OutputCache data will be displayed for the set time duration 

        // For [OutputCache(Duration = 30, Location = OutputCacheLocation.Server )], the output cache will be stored at the Server side

        // For [OutputCache(Duration = 30, Location = OutputCacheLocation.Client )], the output cache will be stored at the Client side (Usually is preferred for links, In client side when we refreshing the website clears client side cache  so it is used for links)

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Server )]
        public ActionResult Index()
        {
            ViewBag.Time = DateTime.Now.ToLongTimeString();

            return View();
        }

        // The result of the action method will be stored in OutputCache for 40 seconds, and the same OutputCache data will be displayed for the set time duration (Modified data won't be reflected till the next 40 seconds)

        [OutputCache(Duration = 40)]
        public ActionResult GetData()
        {
            var data = db.People.ToList();
            
            return View(data);
        }
    }
}