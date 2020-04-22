using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class PartyBusiness : IPartyBusiness
    {
        readonly IPartyRepository partyRepository;
        public PartyBusiness(IPartyRepository partyRepository)
        {
            this.partyRepository = partyRepository;
        }
        public Party AddParty(PartyRequest partyRequest, int adminId)
        {
            if(partyRequest!=null && adminId!=0)
            {
                return this.partyRepository.AddParty(partyRequest, adminId);
            }
            return null;
        }

        public bool DeleteParties(DeletePartiesRequest deletePartiesRequest, int adminId)
        {
            if (deletePartiesRequest != null && adminId != 0)
            {
                return this.partyRepository.DeleteParties(deletePartiesRequest, adminId);
            }
            return false;
        }

        public bool DeleteParty(int partyId, int adminId)
        {
            if (partyId != 0 && adminId != 0)
            {
                return this.partyRepository.DeleteParty(partyId, adminId);
            }
            return false;
        }

        public List<Party> GetAllParties()
        {
            return this.partyRepository.GetAllParties();
        }

        public Party UpdateParty(int partyId, PartyRequest partyRequest, int adminId)
        {
            if (partyRequest != null && adminId != 0 && partyId != 0)
            {
                return this.partyRepository.UpdateParty(partyId, partyRequest, adminId);
            }
            return null;
        }
    }
}
