using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace _11_Cookies.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            // Retrieving a cookie

            HttpCookie cookie = Request.Cookies["Username"];

            if (cookie != null)
            {
                ViewBag.Message = Request.Cookies["Username"].Value.ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}