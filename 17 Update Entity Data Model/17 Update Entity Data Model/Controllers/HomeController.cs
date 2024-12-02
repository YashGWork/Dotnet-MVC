using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _17_Update_Entity_Data_Model.Models;

namespace _17_Update_Entity_Data_Model.Controllers
{
    public class HomeController : Controller
    {

        // Adding the context class object which will contain both the db (student and employee)
        UpdateEDMdbEntities db = new UpdateEDMdbEntities();

        // GET: Home
        public ActionResult Student()
        {
            var data = db.students.ToList();

            return View(data);
        }

        public ActionResult Employee()
        {
            var data = db.employees.ToList();

            return View(data);
        }

    }
}