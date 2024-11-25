using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5_Session.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Creating a Session
            Session["Data"] = "Session persists for 20 minutes";

            ViewData["Var1"] = "Data comes from ViewData";

            ViewBag.Var2 = "Data comes from ViewBag";

            TempData["Var3"] = "Data comes from TempData";

            string[] DataManagement = { "ViewData", "ViewBag", "TempData", "Session" };

            Session["DataMangement"] = DataManagement;

            return View();
        }

        public ActionResult About()
        {
            if (Session["Data"] != null)
            {
                Session["Data"].ToString();
            }
            return View();
        }

        public ActionResult Contact()
        {

            if (Session["Data"] != null)
            {
                Session["Data"].ToString();
            }
            return View();
        }
    }
}