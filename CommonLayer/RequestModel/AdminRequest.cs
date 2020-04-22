using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
   public class AdminRequest
    {
        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "FirstName should contain atleast 2 or more characters")]
        public string AdminFirstName { get; set; }


        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "FirstName should contain atleast 2 or more characters")]
        public string AdminLastName { get; set; }


        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter contact number in correct format")]
        [Required(ErrorMessage ="contact number is required")]
        public long AdminContactNumber { get; set; }


        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter email in correct format")]
        [Required(ErrorMessage ="Email address is required")]
        public string AdminEmail { get; set; }

      
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "Enter strong password")]
        public string Password { get; set; }
    }
}
