using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using _9_b_Entity_Framework.Models;
using System.Data.Entity;

namespace _9_b_Entity_Framework.Controllers
{

    public class HomeController : Controller
    {


              DatabaseFirstEFEntities db = new DatabaseFirstEFEntities();


        // GET: Home
        public ActionResult Index()
        {
            // Getting the data from the table

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
            if (ModelState.IsValid == true)
            {
                // Adding the student into the db and saving it

                db.Students.Add(s);

                int check = db.SaveChanges();

                if (check > 0)
                {
                    TempData["AddStatus"] = "<script> alert('Data Inserted')</script>";

                    return RedirectToAction("Index");
                }
                else
                {
                    {
                        TempData["AddStatus"] = "<script> alert('Data Not Inserted, Please Try Again')</script>";
                    }

                }
            }
            else
            {
                TempData["AddStatus"] = "<script> alert('Data Not Inserted, Please Enter Valid Data')</script>";
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            // Getting the row to be edited and passing it on to be edited

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(Student s)
        {

            if (ModelState.IsValid == true)
            {
                // Saving the modified data

                db.Entry(s).State = EntityState.Modified;
            

            int check = db.SaveChanges();

            if (check > 0)
            {
                TempData["EditStatus"] = "<script>alert('Data successfully edited')</script>";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["EditStatus"] = "<script>alert('Data Not edited, Please Try Again')</script>";
            }
        }
            else
            {
                TempData["EditStatus"] = "<script>alert('Data Not edited, Please Enter Valid Data')</script>";
            }
    

            return View();
        }

        // To get a confirmation for delete action

        /*

        public ActionResult Delete(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(Student s)
        {
            // Deleting the row and saving the changes

            db.Entry(s).State = EntityState.Deleted;

            int check = db.SaveChanges();

            if (check > 0)
            {
                TempData["DeleteStatus"] = "<script>alert('Deletion Successful')</script>";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["DeleteStatus"] = "<script>alert('Deletion Not Successful, Please Try Again')</script>";
            }

            return View();
        }

        */

        // To perform deletion without confirmation

        public ActionResult Delete(int id)
        {
            // Getting the row to be deleted

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();

            // Deleting the row and saving changes

            if (row != null)
            {
                db.Entry(row).State = EntityState.Deleted;

                int check = db.SaveChanges();

                if (check > 0)
                {
                    TempData["DeleteStatus"] = "<script>alert('Deletion Successful')</script>";
                }
                else
                {
                    TempData["DeleteStatus"] = "<script>alert('Deletion Not Successful, Please Try Again')</script>";
                }
            }
            else
            {
                TempData["DeleteStatus"] = "<script>alert('Please select a valid row to delete')</script>";
            }


            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {

            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();


            return View(row);
        }

    }
}