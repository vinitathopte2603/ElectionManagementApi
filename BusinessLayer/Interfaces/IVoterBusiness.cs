using CommonLayer.Model;
using CommonLayer.RequestModel;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IVoterBusiness
    {
        Voter Vote(VoterRequest voterRequest);
        bool DeleteVoter(int voterId, int adminId);
        Voter UpdateVoter(int voterId, VoterDataRequest voterDataRequest, int adminId);
        List<Voter> GetAllVoters();
    }
}
