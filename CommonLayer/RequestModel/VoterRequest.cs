using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class VoterRequest
    {
        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "FirstName should contain atleast 2 or more characters")]
        public string VoterFirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "LastName should contain atleast 2 or more characters")]
        public string VoterLastName { get; set; }


        [Required(ErrorMessage = "Voter Unique id is required")]
        [RegularExpression(@"^([0-9]{1,5})$", ErrorMessage = "Enter valid Id")]
        public long UniqueVoterId { get; set; }


        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter contact number in correct format")]
        public long VoterContactNUmber { get; set; }


        [Required(ErrorMessage = "Candidate id is required")]
        [RegularExpression(@"^([0-9]{1,5})$", ErrorMessage = "Enter valid Id")]
        public int CandidateId { get; set; }

    }
}
