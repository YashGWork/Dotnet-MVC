using _16_Image_CRUD_App.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace _16_Image_CRUD_App.Controllers
{

    public class HomeController : Controller
    {
        // Creating context class object in order to interact with the db model

        ImagesCRUDEntities db = new ImagesCRUDEntities();

        // GET: Home
        public ActionResult Index()
        {
            // Retrieve all employee records from the database and convert them into a list.

            var data = db.employees.ToList();

            // Pass the list of employees to the view for rendering.
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(employee e)
        {
            // Check if the model state is valid before proceeding with the file upload and database operations.

            if (ModelState.IsValid == true)
            {
                // Get the file name without the extension from the uploaded file.
                string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);

                // Get the file extension of the uploaded file.
                string extension = Path.GetExtension(e.ImageFile.FileName);

                // Store the uploaded file in a variable for further processing.
                HttpPostedFileBase postedFile = e.ImageFile;

                // Get the length of the uploaded file.
                int length = postedFile.ContentLength;

                // Validate the file extension and size.
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    // Check if the file size is less than or equal to 1MB.
                    if (length <= 1000000)
                    {
                        // Combine the file name with its extension.
                        fileName = fileName + extension;

                        // Set the image path for the employee record.
                        e.image_path = "~/Images/" + fileName;

                        // Create the full path to save the uploaded file on the server.
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

                        // Save the uploaded file to the specified path.
                        e.ImageFile.SaveAs(fileName);

                        // Add the new employee record to the database context.
                        db.employees.Add(e);

                        // Save changes to the database and check if the operation was successful.
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            // Set a success message in TempData to be displayed after redirection.
                            TempData["successMessage"] = "<script>alert('Form submitted successfully')</script>";

                            // Clear the model state to prepare for a new entry.
                            ModelState.Clear();

                            // Redirect to the Index action of the Home controller.
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Set an error message if the form submission failed.
                            TempData["successMessage"] = "<script>alert('Form not submitted, Please Try Again')</script>";
                        }


                    }
                    else
                    {
                        // Set a message if the uploaded file exceeds the size limit.
                        TempData["sizeMessage"] = "<script>alert('Please only use file below 1MB')</script>";
                    }
                }
                else
                {
                    // Set a message if the uploaded file has an invalid extension
                    TempData["extMessage"] = "<script>alert('Please only use valid file extensions(.jpg, .jpeg or .png)')</script>";
                }


            }


            return View();
        }


        public ActionResult Edit(int id)
        {
            // Retrieve the employee record from the database using the provided ID.
            var EmployeeRow = db.employees.Where(model => model.id == id).FirstOrDefault();

            // Store the current image path in the session for potential use during the edit process.
            Session["Image"] = EmployeeRow.image_path;

            // Pass the employee record to the view for editing.
            return View(EmployeeRow);
        }

        [HttpPost]
        public ActionResult Edit(employee e)
        {
            // Check if the model state is valid before proceeding with the update.
            if (ModelState.IsValid == true)
            {

                // Check if the user has uploaded a new image (update image).
                if (e.ImageFile != null)
                {
                    // Get the file name without the extension from the uploaded file.
                    string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);

                    // Store the uploaded file in a variable for further processing.
                    string extension = Path.GetExtension(e.ImageFile.FileName);

                    // Store the uploaded file in a variable for further processing.
                    HttpPostedFileBase postedFile = e.ImageFile;

                    // Get the length of the uploaded file.
                    int length = postedFile.ContentLength;

                    // Validate the file extension of the uploaded image.
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        // Check if the image size is less than or equal to 1MB.
                        if (length <= 1000000)
                        {
                            // Combine the file name with its extension.
                            fileName = fileName + extension;

                            // Set the image path for the employee record.
                            e.image_path = "~/Images/" + fileName;

                            // Create the full path to save the uploaded file on the server.
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

                            // Save the uploaded file to the specified path.
                            e.ImageFile.SaveAs(fileName);

                            // Mark the employee record as modified in the database context.
                            db.Entry(e).State = EntityState.Modified;
                            int check = db.SaveChanges();

                            if (check > 0)
                            {


                                // Delete the original image stored in the Images folder
                                string ImagePath = Request.MapPath(Session["Image"].ToString());

                                // Check if the original image path exists before attempting to delete it.
                                if (System.IO.File.Exists(ImagePath))
                                {
                                    // Delete the original image file.
                                    System.IO.File.Delete(ImagePath);
                                }

                                // Set a success message in TempData to be displayed after redirection.
                                TempData["updateMessage"] = "<script>alert('Entry updated successfully')</script>";

                                // Clear the model state to prepare for a new entry.
                                ModelState.Clear();

                                // Redirect to the Index action of the Home controller.
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                // Set an error message if the form submission failed.
                                TempData["updateMessage"] = "<script>alert('Form not submitted, Please Try Again')</script>";
                            }

                        }
                        else
                        {
                            // Set a message if the uploaded file exceeds the size limit.
                            TempData["updateSizeMessage"] = "<script>alert('Please only use file below 1MB')</script>";
                        }
                    }
                    else
                    {
                        // Set a message if the uploaded file has an invalid extension.
                        TempData["updateExtMessage"] = "<script>alert('Please only use valid file extensions(.jpg, .jpeg or .png)')</script>";
                    }
                }
                // If the user doesn't update the image, retain the original image path.
                else
                {
                    // Store the original image path as the image was not edited by the user.
                    e.image_path = Session["Image"].ToString();

                    // Mark the employee record as modified in the database context.
                    db.Entry(e).State = EntityState.Modified;
                    int check = db.SaveChanges();

                    if (check > 0)
                    {
                        // Set a success message in TempData to be displayed after redirection.
                        TempData["updateMessage"] = "<script>alert('Entry updated successfully')</script>";

                        // Clear the model state to prepare for a new entry.
                        ModelState.Clear();

                        // Redirect to the Index action of the Home controller.
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Set an error message if the form submission failed.
                        TempData["updateMessage"] = "<script>alert('Form not submitted, Please Try Again')</script>";
                    }
                }
            }

            // Return the view to allow the user to correct any issues with the form submission.
            return View();
        }

        public ActionResult Delete(int id)
        {
            // Check if the provided ID is valid (greater than 0).
            if (id>0)
            {
                // Retrieve the employee record from the database using the provided Id
                var EmployeeRow = db.employees.Where(model => model.id == id).FirstOrDefault();

                // Check if the employee record exists
                if (EmployeeRow!=null)
                {
                    // Mark the employee record as deleted in the database context.
                    db.Entry(EmployeeRow).State = EntityState.Deleted;
                   int check = db.SaveChanges();

                    if(check>0)
                    {
                        // Set a success message in TempData to be displayed after redirection.
                        TempData["deleteMessage"] = "<script>alert('Entry deleted successfully')</script>";

                        // Also delete the associated image from the Images folder.
                        string ImagePath = Request.MapPath(EmployeeRow.image_path.ToString());

                        // Check if the image path exists before attempting to delete it.
                        if (System.IO.File.Exists(ImagePath))
                        {
                            // Delete the image file from the server.
                            System.IO.File.Delete(ImagePath);
                        }

                    }
                    else
                    {
                        // Set an error message if the entry deletion failed.
                        TempData["deleteMessage"] = "<script>alert('Entry not deleted, Please try again')</script>";
                    }
                }
            }
            // Redirect to the Index action of the Home controller after deletion.
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            // Retrieve the employee record from the database using the provided ID.
            var EmployeeRow = db.employees.Where(model => model.id == id).FirstOrDefault();

            // Store the image path of the employee in the session to display the image in the view instead of image path.
            Session["Image2"] = EmployeeRow.image_path.ToString();

            // Check if the employee record exists.
            if (EmployeeRow!=null) {
                // Pass the employee record to the view for displaying details.
                return View(EmployeeRow);
             }

            // Set an error message in TempData if the employee record is not found.
            TempData["detailsMessage"] = "<script>alert('Row not found, Please refresh the page')</script>";

            // Redirect to the Index action of the Home controller if the record is not found.
            return RedirectToAction("Index", "Home");
        }
    }
}