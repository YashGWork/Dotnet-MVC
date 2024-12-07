//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _15_Uploding_and_Retrieving_Images.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class student
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Standard")]
        public Nullable<int> standard { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Image")]
        public string image_path { get; set; }

        // Extra Model Property to save  uploaded image file to a folder 
      public HttpPostedFileBase ImageFile { get; set; }
    }

}
