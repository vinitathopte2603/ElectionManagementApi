using CommonLayer.Model;
using CommonLayer.RequestModel;
using System.Collections.Generic;


namespace RepositoryLayer.Interfaces
{
    public interface IVoterRepository
    {
        Voter Vote(VoterRequest voterRequest);
        bool DeleteVoter(int voterId, int adminId);
        Voter UpdateVoter(int voterId, VoterDataRequest voterDataRequest, int adminId);
        List<Voter> GetAllVoters();
    }
}
