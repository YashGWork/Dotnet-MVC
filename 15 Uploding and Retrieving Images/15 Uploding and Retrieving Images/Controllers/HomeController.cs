using _15_Uploding_and_Retrieving_Images.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace _15_Uploding_and_Retrieving_Images.Controllers
{
    public class HomeController : Controller
    {
        // Creating Db context object to interact with the database

        ImagesDBEntities db = new ImagesDBEntities();

        // To view the list
        public ActionResult Index()
        {

            var data = db.students.ToList();

            return View(data);
        }







        // GET: Home
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(student s)
        {
            // Storing image file name

            string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);


            string fileExtension = Path.GetExtension(s.ImageFile.FileName);

            // Holds the image uploaded by the user
            HttpPostedFileBase postedFile = s.ImageFile;

            // Get's the size of the image
            int length = postedFile.ContentLength;

            if(fileExtension.ToLower()==".jpg"||fileExtension.ToLower() == ".jpeg"|| fileExtension.ToLower() == ".png")
            {
                if(length<=1000000)
                {

                    fileName = fileName + fileExtension;

                    s.image_path = "~/Images/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

                    s.ImageFile.SaveAs(fileName);

                    db.students.Add(s);

                    int check = db.SaveChanges();


                    // Check if DB changes are successful or not
                    if (check > 0)
                    {
                        ViewBag.Message = "<script>alert('Form submitted successfully')</script>";

                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "<script>alert('Form not submitted, Pls try again')</script>";
                    }
                }
                else
                {
                    ViewBag.sizeMessage = "<script>alert('Image should be less than 1MB')</script>";
                }
            }
            else
            {
                ViewBag.extMessage = "<script>alert('This image extension is not supported, Please try with .jpg, .png or .jpeg images')</script>";
            }

            return View();
        }
    }
}