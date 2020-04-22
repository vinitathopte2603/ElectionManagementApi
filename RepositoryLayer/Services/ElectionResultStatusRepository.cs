using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ElectionResultStatusRepository : IElectionResultStatusRepository
    {
        readonly DBContext dBContext;
        public ElectionResultStatusRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public List<ConstituencyWiseResponse> ConstituencyWiseResult(int constituencyId)
        {
            var candidatesList = (from c in this.dBContext.candidates
                                  join p in this.dBContext.parties
                                  on c.PartyId equals p.PartyId
                                  where c.ConstituencyId == constituencyId
                                  select new ConstituencyWiseResponse()
                                  {
                                      CandidateName = c.CandidateFirstName + " " + c.CandidateLastName,
                                      PartyName = p.PartyName,
                                      Votes = c.Votes
                                  }
                                ).ToList();
            if (candidatesList != null)
            {
                return candidatesList;
            }
            return null;
        }

        public List<PartyWiseResponse> PartyWiseResult(string state)
        {
            var constituencies = this.dBContext.constituencies.Where(linq => linq.State == state);
            List<PartyWiseResponse> partyWiseResponses = new List<PartyWiseResponse>();
            foreach (var constituency in constituencies)
            {
                var newTabel = (from can in this.dBContext.candidates
                                join p in this.dBContext.parties
                                on can.PartyId equals p.PartyId
                                where can.ConstituencyId == constituency.ConstituencyId

                                select new PartyWiseResponse()
                                {
                                    PartyName = p.PartyName,
                                    Votes = can.Votes
                                }
                                ).ToList();
                foreach (var tabel in newTabel)
                {
                    bool flag = false;
                    foreach (var partyWiseResponse in partyWiseResponses)
                    {
                        if (tabel.PartyName == partyWiseResponse.PartyName)
                        {
                            partyWiseResponse.Votes = tabel.Votes + partyWiseResponse.Votes;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        partyWiseResponses.Add(tabel);
                    }
                }
            }
            return partyWiseResponses;
        }
    }
}
