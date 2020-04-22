using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
   public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }


        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]*{2,})$", ErrorMessage = "FirstName should contain atleast 2 or more characters")]
        public string AdmFirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]*{2,})$", ErrorMessage = "LastName should contain atleast 2 or more characters")]
        public string AdmLastName { get; set; }


        [Required(ErrorMessage ="Contact number is required")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter contact number in correct format")]
        public long AdmContactNumber { get; set; }


        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter email in correct format")]
        [Required(ErrorMessage ="Email address is required")]
        public string AdmEmail { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "Enter strong password")]
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
