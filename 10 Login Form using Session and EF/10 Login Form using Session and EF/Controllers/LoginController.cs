using _10_Login_Form_using_Session_and_EF.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _10_Login_Form_using_Session_and_EF.Controllers
{

    // Valid Credentials - Username:Yash Password: qwerty123
    public class LoginController : Controller
    {
        // Creating a context class object of DB Context to access the database
        private readonly LoginDBEntities db = new LoginDBEntities();

        // GET: Login - Displays the login view
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login - Handles the login form submission
        [HttpPost]
        public ActionResult Index(LoginTableDB logininfo)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Attempt to find the user credentials in the database
                var credentials = db.LoginTableDBs
                    .FirstOrDefault(model => model.username == logininfo.username && model.password == logininfo.password);

                // If no matching username and password are found
                if (credentials == null)
                {
                    ViewBag.ErrorMessage = "Login Failed, Please Try Again";
                }
                else
                {
                    // Store the username in the session to maintain user state
                    Session["username"] = logininfo.username;

                    // Redirect to the home controller's index action method upon successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we reach here, something went wrong; return the view with the current model state
            return View(logininfo);
        }

        // Action to log out the user
        public ActionResult Logout()
        {
            // Clear the session to log out the user
            Session.Abandon();

            // Redirect to the login page after logout
            return RedirectToAction("Index", "Login");
        }

        // Action to reset the login form
        public ActionResult Reset()
        {
            // Clear the model state to reset the form fields
            ModelState.Clear();

            // Redirect to the login page
            return RedirectToAction("Index", "Login");
        }
    }
}
