using _5_Data_State_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace _5_Data_State_Management.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            string[] course = { "HTML", "CSS", "JS", "C#", ".NET"};
            string[] tools = { "Github", "Git", "VS Code", "Visual Studio" };


            Employee emp = new Employee();
            emp.Name = "Yash";
            emp.Designation = "Software Engineer";
            emp.EmployeeID = 23;
            emp.Salary = 900000;
            emp.YOE = 7;

            // Using ViewData

            // Passing an array in ViewData
            ViewData["course"] = course;

            // Passing Model data in ViewData
            ViewData["emp"] = emp;

            ViewData["VDMessage"] = "Message from ViewData";

            // Using ViewBag

            // Passing an array in ViewBag
            ViewBag.Tools = tools;

            ViewBag.VBMessage = "Message from ViewBag";

            ViewBag.CommonMessage = "Data between ViewBag and ViewData can be accessed by each other interchangeably";

            // Using TempData

            TempData["TDMessage"] = "Message from TempData";

            TempData.Keep("TDMessage");

            return View();
        }

        public ActionResult Next()
        {
            // To further keep TempData to the next request
            TempData.Keep("TDMessage");
            return View();
        }

        public ActionResult Second()
        {
            return View();
        }
    }
}