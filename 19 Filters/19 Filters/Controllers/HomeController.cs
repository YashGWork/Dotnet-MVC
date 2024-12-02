using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19_Filters.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        [HandleError]
        public ActionResult Index()
        {
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


            return View();
        }


        public ActionResult ErrorPage()
        {

            return View();
        }
    }
}