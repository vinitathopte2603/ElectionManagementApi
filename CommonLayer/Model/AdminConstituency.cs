using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminConstituency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminConstId { get; set; }


        [ForeignKey("admins")]
        public int AdminId { get; set; }


        [ForeignKey("constituencies")]
        public int ConstituencyId { get; set; }
    }
}
