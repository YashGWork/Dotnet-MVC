using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using _12_Remember_Me_Functionality.Models;
using System.Web.Configuration;

namespace _12_Remember_Me_Functionality.Controllers
{
    public class LoginController : Controller
    {
        // Goal is to store username & password in cookie when Remember me is checked and use it to re-login

        // Creating context class object to interact with DB
        LoginDBEntities db = new LoginDBEntities();

        public ActionResult Index()
        {
            // Login if cookie is available

            // Requesting a cookie named User
            HttpCookie cookie = Request.Cookies["User"];

            // If cookie is not null (that means it contain username and password)
            if (cookie != null)
            {
                // Retreiving Username and Password from cookie (To be sent to Index view to login)

                ViewBag.username = cookie["username"].ToString();

                // Decrypting the user password stored in cookie

                string EncryptPassword = cookie["password"].ToString();
                byte[] b = Convert.FromBase64String(EncryptPassword);

                string decryptPassword = ASCIIEncoding.ASCII.GetString(b);

                // Sending the decrypted password for logging in

                ViewBag.password = decryptPassword;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid == true)
            {
                // Creating a cookie named User
                HttpCookie cookie = new HttpCookie("User");

                // If Remember Me checkbox was checked
                if (user.RememberMe == true)
                {

                    // Saving the user Username & Password in cookie
                    cookie["username"] = user.username;

                    // Encrypting (Encoding) the password before storing it in the cookie

                    byte[] b = ASCIIEncoding.ASCII.GetBytes(user.password);

                    string EncryptedPassword = Convert.ToBase64String(b);

                    // Storing the Encrypted Password
                    cookie["password"] = EncryptedPassword;

                    // Setting the cookie expiration date as 2 days
                    cookie.Expires = DateTime.Now.AddDays(2);

                    // Saving the cookies in current browser
                    HttpContext.Response.Cookies.Add(cookie);

                }
                // If user hasn't checked Remember Me checkbox
                else
                {
                    // Will destroy the created cookie
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(cookie);
                }

                // Gets the row if Username and password matchess

                var row = db.Users.Where(model => model.username == user.username && model.password == user.password).FirstOrDefault();

                // If row is not null
                if (row != null)
                {
                    Session["Username"] = user.username;
                    TempData["Message"] = "<script>alert('You have succesfully logged in')</script>";

                    // Now redirecting to Dashboard after successfull login

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["Message"] = "<script>alert('Login Failed, Pls Try Again')</script>";
                }

            }


            return View();
        }
    }
}