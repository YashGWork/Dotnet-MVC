using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12_Remember_Me_Functionality.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            // If there was no login then redirect to login page

            if (Session["username"]== null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public ActionResult Logout()
        {
            // Destroying the session when user is logging out

            if (Session["username"] != null)
            {
                
                // Destroying the session

Session.Abandon();
            }


            // Redirecting the User to the Login form
            return RedirectToAction("Index", "Login");
        }
    }
}