//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _20_Authorize_Filter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [Required(ErrorMessage = "This field cannot be empty")]
        [DisplayName("User Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
