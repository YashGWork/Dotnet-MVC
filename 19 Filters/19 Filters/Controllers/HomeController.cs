using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19_Filters.Controllers
{
    // Exception Filter at controller level
    [HandleError]
    // Custom View for Exception Filter at controller level
    // [HandleError (View = "Error2")]
    public class HomeController : Controller
    {
        // Exceptions


        /* 
         Divide by Zero Exception

         int a = 10;
         int b = 5;
         int d = 0;
         int c = a / b;

         int c = a / d;
         */


        /*
         Null Reference Exception

         string a = null;
         int b = a.Length; 
        */

        /*
         Index Out of Range Exception

        int[] a = new int[3];
        a[0] = 11;
        a[1] = 22;
        a[2] = 33;
        a[3] = 44;
        a[4] = 55;
        */

        /*
          To Throw Custom Exception

           throw new Exception();
        */

        /*
         Executing code that could throw error

        try
        {
           throw new Exception();
        }
        catch(Exception ex)
        {
           return RedirectionToAction("ErrorPage","Home");
        }

        */

        // Exception Filter at action level
        [HandleError]
        public ActionResult Index()
        {
            throw new Exception();

            return View();
        }


        // Overloaded version of HandleError to show customized view at action level
        [HandleError (View = "Error2")]
        // Handle Error in case not use so will show default error page
        public ActionResult About()
        {
            throw new Exception();

            return View();
        }
    }
}