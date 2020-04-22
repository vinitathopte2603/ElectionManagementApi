using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class VoterDataRequest
    {
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "Name should contain atleast 2 or more characters")]
        public string VoterFirstName { get; set; }
     

        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "Name should contain atleast 2 or more characters")]
        public string VoterLastName { get; set; }


        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter contact number in correct format")]
        public long VoterContactNUmber { get; set; }

    }
}
