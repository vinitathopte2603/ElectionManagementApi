using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminPartyId { get; set; }


        [ForeignKey("admins")]
        public int AdminId { get; set; }


        [ForeignKey("parties")]
        public int PartyId { get; set; }
    }
}
