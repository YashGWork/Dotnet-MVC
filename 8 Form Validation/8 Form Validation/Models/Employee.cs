using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace _8_Form_Validation.Models
{
    public class Employee
    {
        // By default int and date time have required data annotations to them

        // To avoid by default data annotations we can use ? operator. For eg

        //public int?EmployeeId {get; set;}


        [DisplayName("Organization")]
        [ReadOnly(true)]
        public int OrganizationName { get; set; }


        [Required(ErrorMessage = "Employee Id is mandatory")]
        [DisplayName("ID")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is mandatory")]
        [StringLength (20, MinimumLength = 5, ErrorMessage = "Name should be atleast 5 and atmax 20 characters")]
        [DisplayName("Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Joining Date is mandatory")]
        [DisplayName("Joining Date")]
        [DataType(DataType.Date)]
        public string EmpJoiningDate
        {  get; set; }


        [Required(ErrorMessage = "Employee Age is mandatory")]
        [Range(20,65, ErrorMessage = "The age should be in between 20 and 65")]
        [DisplayName("Age")]
        public int EmployeeAge { get; set; }

        [Required(ErrorMessage = "Employee Gender is mandatory")]
        [DisplayName("Gender")]
        public string EmployeeGender { get; set; }

        [Required(ErrorMessage = "Employee Email is mandatory")]
        [RegularExpression("^[a-zA-Z0-9_.±]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Please Enter a valid email")]
        [DisplayName("Email")]
        public string EmployeeEmail { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Reenter the password for confirmation")]
        [Compare("Password", ErrorMessage = "The password doesn't match")]
        [DisplayName("Reenter Password")]
        public string ConfirmPassword { get; set; }
    }
}