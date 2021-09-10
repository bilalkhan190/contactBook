using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContacBookApp.Models
{
    
    public class ContactMeta
    {
        public long ID { get; set; }
        [Required(ErrorMessage ="this field is required")]
        public string FullName { get; set; }

        public string NickName { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Status { get; set; }
    }

    public class LoginMeta
    {
        [Required(ErrorMessage ="this field is required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public string RememberMe { get; set; }
    }

    public class RegisterMeta
    {
        [Required]
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

}