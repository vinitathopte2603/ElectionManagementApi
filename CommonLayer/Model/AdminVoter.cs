using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminVoter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminVoterId { get; set; }


        [ForeignKey("admins")]
        public int AdminId { get; set; }


        [ForeignKey("voters")]
        public int VoterId { get; set; }
    }
}
