using CommonLayer.Model;
using CommonLayer.RequestModel;
using System.Collections.Generic;


namespace RepositoryLayer.Interfaces
{
    public interface IPartyRepository
    {
        Party AddParty(PartyRequest partyRequest, int adminId);

        bool DeleteParty(int partyId, int adminId);

        Party UpdateParty(int partyId, PartyRequest partyRequest, int adminId);

        List<Party> GetAllParties();
        bool DeleteParties(DeletePartiesRequest deletePartiesRequest, int adminId);
    }
}
