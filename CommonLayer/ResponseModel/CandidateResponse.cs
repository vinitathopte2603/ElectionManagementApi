using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CandidateResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateId { get; set; }

        public string CandidateFirstName { get; set; }

        public string CandidateLastName { get; set; }
        public long CandidatePhoneNumber { get; set; }
        public int PartyId { get; set; }

        public List<NameResponse> PartyName { get; set; }

        public int ConstituencyId { get; set; }

        public List<NameResponse> ConstituencyName { get; set; }
        public int Votes { get; set; }
        public List<AdminResponse> AdminResponses { get; set; }
    }
}
