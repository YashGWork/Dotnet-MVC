using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10_Login_Form_using_Session_and_EF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            // Check if the user is logged in by verifying the session
            if (Session["username"] == null)
            {
                // If the user is not logged in, redirect them to the login form
                return RedirectToAction("Index", "Login");
            }

            // If the user is logged in, display the home view
            return View();
        }
    }
}