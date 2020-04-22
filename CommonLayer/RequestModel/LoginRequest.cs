using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class LoginRequest
    {
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter email in correct format")]
        [Required(ErrorMessage = "Email address is required")]
        public string AdminEmail { get; set; }


        [Required(ErrorMessage ="Password is required")]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "Enter strong password")]
        public string Password { get; set; }
    }
}
