using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class ConstituencyRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "Name should contain atleast 2 or more characters")]
        public string ConstituencyName { get; set; }


        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "City should contain atleast 2 or more characters")]
        public string City { get; set; }


        [Required(ErrorMessage = "State is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "State should contain atleast 2 or more characters")]
        public string State { get; set; }
    }
}
