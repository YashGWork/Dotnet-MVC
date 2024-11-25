using System;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using _8_Form_Validation.Models;

namespace _8_Form_Validation.Controllers
{
    public class HomeController : Controller
    {
        // Regular expression pattern for validating email addresses.
        // It checks that the email follows the general pattern "name@domain.extension"
        // where the extension is between 2 and 9 characters long.
        private readonly string emailVerificationPattern = "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,9})$";

        // GET: Home
        // Displays the initial form view to the user.
        public ActionResult Index()
        {
            return View();
        }

        // POST: Home/Index
        // Handles form submission with validation for each input field: FullName, Age, and Email.
        [HttpPost]
        public ActionResult Index(string FullName, string Age, string Email)
        {
            // **Full Name Validation**
            // Check if the FullName field is empty or null.
            if (string.IsNullOrEmpty(FullName))
            {
                // Add a model state error for FullName, which will be displayed in the view.
                ModelState.AddModelError("FullName", "FullName field cannot be empty!");

                // Set a ViewBag variable to display an asterisk (*) next to the FullName input field
                // as a visual indication that there's an error.
                ViewBag.FNErrorMsg = "*";
            }

            // **Age Validation**
            // Check if the Age field is empty or null.
            if (string.IsNullOrEmpty(Age))
            {
                // Add a model state error for Age if the field is empty.
                ModelState.AddModelError("Age", "Age field cannot be empty!");

                // Set a ViewBag variable to display an asterisk (*) next to the Age input field.
                ViewBag.AErrorMsg = "*";
            }

            // **Email Validation**
            // Check if the Email field is empty or null.
            if (string.IsNullOrEmpty(Email))
            {
                // Add a model state error for Email if the field is empty.
                ModelState.AddModelError("Email", "Email field cannot be empty!");

                // Set a ViewBag variable to display an asterisk (*) next to the Email input field.
                ViewBag.EErrorMsg = "*";
            }
            else
            {
                // If the Email field is not empty, validate it against the email regex pattern.
                // This checks if the entered email follows the correct format (e.g., "user@example.com").
                if (!Regex.IsMatch(Email, emailVerificationPattern))
                {
                    // Add a model state error if the Email does not match the expected format.
                    ModelState.AddModelError("Email", "Email is invalid!");

                    // Set a ViewBag variable to display an asterisk (*) next to the Email input field.
                    ViewBag.EErrorMsg = "*";
                }
            }

            // **Form Submission**
            // If ModelState is valid (i.e., no errors in the fields), proceed with form submission logic.
            if (ModelState.IsValid)
            {
                // Display a success message to the user in the form of an alert.
                // This is embedded in ViewBag and will be executed as JavaScript in the view.
                ViewBag.StatusMsg = "<script> alert('Form was successfully submitted'); </script>";

                // Clear ModelState to reset the form after successful submission,
                // so any previous input values and errors are cleared.
                ModelState.Clear();
            }

            // Return the view to the user. If there are validation errors, they will be displayed
            // based on the ModelState and ViewBag error messages set above.
            return View();
        }

        public ActionResult DataAnnotations()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DataAnnotations(Employee obj)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Msg = "<script>alert('Form Submitted Successfully') </script>";

                ModelState.Clear();
            }

            return View();
        }
    }
}
