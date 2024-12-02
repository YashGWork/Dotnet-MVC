using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace _9_Entity_Framework.Models
{
    public class Student
    {
       // Marking Id as PrimaryInteropAssemblyAttribute key

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set ; }

        public int Age { get; set; }

    }
}