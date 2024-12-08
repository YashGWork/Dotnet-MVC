using _20_Authorize_Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _20_Authorize_Filter.Controllers
{
    public class LoginController : Controller
    {

        // Creating a Context class object in order to interact with the db

        LoginDBEntities db = new LoginDBEntities();


        // GET: Login
        // First creating a database in App_Data folder and then creating model using Entity Data Framework (Database First Approach)

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u, string ReturnUrl)
        {
            // The link also automatically contains Return Url of [Home/Contact] as it is redirected from contact action method of home controller [Home/Contact]

            // If the user login is successful, then the user should be redirected to Home/Contact (Return Url)

            if (IsValid(u) == true)
            {
                // Creating a temporary cookie (ASPXAUTH cookie) for the user to login/authenticate

                FormsAuthentication.SetAuthCookie(u.Username,false);

                Session["username"] = u.Username.ToString();

                if(ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            // If the credentials aren't valid
            else
            {
                TempData["LoginError"] = "<script> alert('Credentials are not valid')</script>";

                Console.WriteLine("Login Failed");

                return View();

            }

        }

        // Public method to autheticate user
        public bool IsValid(User u)
        {

            // Checking whether the entered credentials are valid or not, if yes getting the entire credentials row from db

            var credentials = db.Users.Where(model => model.Username == u.Username && model.Password == u.Password).FirstOrDefault();

            bool auth_status = (credentials != null) ? true : false;

            return auth_status;
        }

        public ActionResult Logout()
        {

            // Signing out using FormsAuthentication method

            FormsAuthentication.SignOut();

            // Destroying the session

            Session["username"] = null;

            return RedirectToAction("Index","Home");
        }
    }
}