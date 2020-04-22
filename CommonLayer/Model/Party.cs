using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class Party
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartyId { get; set; }


        [Required(ErrorMessage = " Party name is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name should contain atleast 2 or more characters")]
        public string PartyName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        [ForeignKey("admins")]
        public int AdminId { get; set; }
        public List<AdminResponse> AdminResponses { get; set; }
    }
}
