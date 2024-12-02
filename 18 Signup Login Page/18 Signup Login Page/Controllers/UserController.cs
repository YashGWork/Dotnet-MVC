using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _18_Signup_Login_Page.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            // If the page is being accessed without login

            if (Session["UserId"]==null)
            {
                return RedirectToAction("Index","Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {

            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }
    }
}