using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class CandidateRequest
    {
        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "FirstName should contain atleast 2 or more characters")]
        public string CandidateFirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "LastName should contain atleast 2 or more characters")]
        public string CandidateLastName { get; set; }


        [ForeignKey("parties")]
        [RegularExpression(@"^([0-9]{1,5})$", ErrorMessage = "Enter valid Id")]
        public int PartyId { get; set; }


        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter contact number in correct format")]
        public long CandidatePhoneNumber { get; set; }


        [ForeignKey("constituencies")]
        [RegularExpression(@"^([0-9]{1,5})$", ErrorMessage = "Enter valid Id")]
        public int ConstituencyId { get; set; }
    }
}
