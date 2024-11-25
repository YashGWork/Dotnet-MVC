using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _11_Cookies.Models;

namespace _11_Cookies.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User s)
        {
            if(ModelState.IsValid == true)
            {
                // Creating a cookie

                HttpCookie cookie = new HttpCookie("Username");

                // Saving username in cookie

                cookie.Value = s.Username;

                // Saving cookie in the browser (sending the response)

                HttpContext.Response.Cookies.Add(cookie);

                // Setting the expired time for a cookie (Creating a persistent cookie)

                cookie.Expires = DateTime.Now.AddDays(2);

                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
    }
}