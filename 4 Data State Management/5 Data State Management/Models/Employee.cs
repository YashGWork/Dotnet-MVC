using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5_Data_State_Management.Models
{
    public class Employee
    {
        public string Name { get; set; }

        public string Designation {  get; set; }

        public int EmployeeID { get; set; }

        public double Salary { get; set; }

        public int YOE { get; set; }
    }
}