using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminCandidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminCandidateId { get; set; }


        [ForeignKey("admins")]
        public int AdminId { get; set; }


        [ForeignKey("candidates")]
        public int CandidateId { get; set; }
    }
}
