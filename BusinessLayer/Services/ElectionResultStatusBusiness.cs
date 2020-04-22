using BusinessLayer.Interfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ElectionResultStatusBusiness : IElectionResultStatusBusiness
    {
        readonly IElectionResultStatusRepository electionResultStatusRepository;
        public ElectionResultStatusBusiness(IElectionResultStatusRepository electionResultStatusRepository)
        {
            this.electionResultStatusRepository = electionResultStatusRepository;
        }
        public List<ConstituencyWiseResponse> ConstituencyWiseResult(int constituencyId)
        {
            if (constituencyId != 0)
            {
                return this.electionResultStatusRepository.ConstituencyWiseResult(constituencyId);
            }
            return null;
        }

        public List<PartyWiseResponse> PartyWiseResult(string state)
        {
            if (state != null)
            {
                return this.electionResultStatusRepository.PartyWiseResult(state);
            }
            return null;
        }
    }
}
