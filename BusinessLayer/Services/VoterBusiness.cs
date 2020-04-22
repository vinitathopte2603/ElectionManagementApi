using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class VoterBusiness : IVoterBusiness
    {
        readonly IVoterRepository voterRepository;
        public VoterBusiness(IVoterRepository voterRepository)
        {
            this.voterRepository = voterRepository;
        }
        public bool DeleteVoter(int voterId, int adminId)
        {
          if(voterId!=0&&adminId!=0)
            {
                return this.voterRepository.DeleteVoter(voterId, adminId);
            }
            return false;
        }

        public List<Voter> GetAllVoters()
        {
            return this.voterRepository.GetAllVoters();
        }

        public Voter UpdateVoter(int voterId, VoterDataRequest voterDataRequest, int adminId)
        {
            if(voterId!=0&&voterDataRequest!=null&&adminId!=0)
            {
                return this.voterRepository.UpdateVoter(voterId, voterDataRequest, adminId);
            }
            return null;
        }

        public Voter Vote(VoterRequest voterRequest)
        {
            if(voterRequest!=null)
            {
                return this.voterRepository.Vote(voterRequest);
            }
            return null;
        }
    }
}
