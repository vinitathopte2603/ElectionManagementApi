using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class Constituency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConstituencyId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "Name should contain atleast 2 or more characters")]
        public string ConstituencyName { get; set; }


        [Required(ErrorMessage = "State is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "State should contain atleast 2 or more characters")]
        public string State { get; set; }


        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^([A-Z][a-zA-Z]{2,})$", ErrorMessage = "City should contain atleast 2 or more characters")]
        public string City { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("admins")]
        public int AdminId { get; set; }
        public List<AdminResponse> AdminResponses { get; set; }
    }
}
