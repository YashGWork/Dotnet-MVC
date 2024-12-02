using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _18_Signup_Login_Page.Models;

// Watch https://youtu.be/4j74uFBBIow?si=Gsz6B5mywd5ZkIoM

namespace _18_Signup_Login_Page.Controllers
{
    public class LoginController : Controller
    {
        SignupLoginEntities db = new SignupLoginEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {

            var user = db.Users.Where(model => model.username == u.username && model.password == u.password).FirstOrDefault();

            if(user!=null)
            {
                Session["UserId"] = u.Id.ToString();

                Session["Username"] = u.username.ToString();

                TempData["LoginSuccessMessage"] = "<script>alert('Login Successful')</script>";

                return RedirectToAction("Index", "User");

            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Login Error, Please Try Again')</script>";

                return View();
            }

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User u)
        {
            if (ModelState.IsValid == true)
            {
                db.Users.Add(u);

                int check = db.SaveChanges();

                if (check > 0)
                {
                    ViewBag.LoginMessage = "<script>alert('Sign Up Successful')</script>";
                }
                else
                {
                    ViewBag.LoginMessage = "<script>alert('Sign Up unsuccessful, Please Try Again')</script>";
                }
            }
            else
            {
                ViewBag.LoginMessage = "<script>alert('Sign Up unsuccessful, Please Try Again')</script>";
            }
     
            return View();
        }
    }
}