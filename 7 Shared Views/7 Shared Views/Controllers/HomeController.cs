using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _7_Shared_Views.Models;

namespace _7_Shared_Views.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StronglyTypedView()
        {
            Employee emp = new Employee();

            emp.Name = "Yash";
            emp.Id = 23;
            emp.Designation = "Software Engineer";
            emp.Salary = 900000;

            ViewData["Var1"] = emp;

            ViewBag.Var2 = emp;

        // Can also pass a list/array/collections of the object in model

            return View(emp);
        }

        public ActionResult PartialView()
        {
            return View();
        }

        public ActionResult StronglyTypedPartialView()
        {
            List<Product> products = new List<Product>()
            {
                new Product {Id = 1, Name = "Perfume", Price = 2500, picture = "~/images/Perfume.jpg"},
                new Product {Id = 2, Name = "Watch", Price = 10000, picture = "~/images/Watch.jpg" },
                new Product {Id = 3, Name = "Belt.jpg", Price = 3000, picture = "~/images/Belt.jpg"},
                new Product{Id = 4, Name = "Shoes.jpg", Price = 5000, picture = "~/images/Shoes.jpg"}
            };

            return View(products);
        }
    }
}