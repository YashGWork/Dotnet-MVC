using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using _9_Entity_Framework.Models;

namespace _9_Entity_Framework.Controllers
{

    public class HomeController : Controller
    {

        StudentContext db = new StudentContext();

        // GET: Home
        public ActionResult Index()
        {
            // data variable get all the rows of the Student table and will be passed on to the Index view

            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            // Adding student s to database

          if(ModelState.IsValid)
            {
                db.Students.Add(s);
                int message = db.SaveChanges();  // Returns 0 if fails, 1 if success

                if (message > 0)
                {
                    // ViewBag.InsertStatus = "<script> alert('Data added successfully') </script>";

                    TempData["InsertStatus"] = "Data added successfully";
                    // ModelState.Clear();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.InsertStatus = "<script>alert('Data couldn't be addded, Please Try Again')</script>";
                }
            }
          else
            {
                ViewBag.InsertStatus = "<script>alert('Data couldn't be addded, Please Enter valid data')</script>";
            }

            return View();
        }
    
        public ActionResult Edit(int id)
        {
            // Getting the data (column) to be edited using LINQ

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(Student s)
        {

            if (ModelState.IsValid == true)
            {
                // Updating the data and saving the changes

                db.Entry(s).State = EntityState.Modified;

                int check = db.SaveChanges();

                if (check > 0)
                {
                    //   ViewBag.EditMessage = "<script> alert('Data Updated') </script>";

                    TempData["EditStatus"] = "Data Updated";

                    //  ModelState.Clear();

                    return RedirectToAction("Index");
                }
                else
                {

                    ViewBag.EditMessage = "<script> alert('Data Not Updated, Please Try Again') </script>";

                }
            }
            else
            {

                ViewBag.EditMessage = "Please Enter the valid data";

            }

    

            return View();
        }

        // If we want a confirmation to delete an entry

        /*
        public ActionResult Delete(int id)
        {

            // Getting the row to be deleted and passing it to the view

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(row);
        }

        [HttpPost]  
        public ActionResult Delete(Student s)
        {
            // Deleting the particular row and saving the changes

            db.Entry(s).State = EntityState.Deleted;

          int check =   db.SaveChanges();

            if (check > 0)
            {
                TempData["DeleteStatus"] = "<script> alert('The row has been deleted') </script>";
            }
            else
            {
                TempData["DeleteStatus"] = "<script> alert('The row hasn't been deleted, Please Try Again') </script>";
            }

            return RedirectToAction("Index");
        }

        */

        // If we directly want to delete the entry

        public ActionResult Delete(int id)
        {

            if(id>0)
            {
                var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
                if(row != null)
                {

                    db.Entry(row).State = EntityState.Deleted;

                    int check = db.SaveChanges();

                    if (check > 0)
                    {
                        //TempData["DeleteStatus"] = "<script> alert('The row has been deleted') </script>";

                        TempData["DeleteStatus"] = "The row has been deleted";
                    }
                    else
                    {
                        //TempData["DeleteStatus"] = "<script> alert('The row hasn't been deleted, Please Try Again') </script>";

                        TempData["DeleteStatus"] = "The row hasn't been deleted, Please Try Again";
                    }
                }
            }
            else
            {
                //TempData["DeleteStatus"] = "<script> alert('Not a valid row') </script>";
                TempData["DeleteStatus"] = "Not a valid row";
            }

            return RedirectToAction("Index");
        }
        

        public ActionResult Details(int id)
        {

            var DetailsById = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(DetailsById);
        }

    }
}