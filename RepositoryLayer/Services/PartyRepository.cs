using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class PartyRepository : IPartyRepository
    {
        readonly DBContext dBContext;
        public PartyRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Party AddParty(PartyRequest partyRequest, int adminId)
        {
            try
            {
                var partyData = this.dBContext.parties.FirstOrDefault(linq => linq.PartyName == partyRequest.PartyName);
                if (partyData == null)
                {
                    Party party = new Party()
                    {
                        PartyName = partyRequest.PartyName,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        IsDeleted = false,
                        AdminId = adminId
                    };
                    this.dBContext.parties.Add(party);
                    this.dBContext.SaveChanges();
                    return party;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteParties(DeletePartiesRequest deletePartiesRequest, int adminId)
        {
            int count;
            foreach(var id in deletePartiesRequest.PartyIds)
            {
                count = Frequency(deletePartiesRequest.PartyIds, id.PartyId);
                if (count % 2 == 1 || count == 1)
                {
                    return DeleteParty(id.PartyId, adminId);
                }
                return false;
            }
            return false;
        }

        public bool DeleteParty(int partyId, int adminId)
        {
            try
            {
                var party = this.dBContext.parties.FirstOrDefault(linq => linq.PartyId == partyId && linq.IsDeleted == false);
                if (party != null)
                {
                    party.AdminId = adminId;
                    party.Modified = DateTime.Now;
                    party.IsDeleted = true;
                    var partyData = this.dBContext.parties.Attach(party);
                    partyData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this.dBContext.SaveChanges();
                    if (partyData.State != 0)
                    {
                        var data = new AdminParty
                        {
                            PartyId = partyId,
                            AdminId = adminId
                        };
                        this.dBContext.adminParties.Add(data);
                        this.dBContext.SaveChanges();

                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Party> GetAllParties()
        {
            List<Party> parties = dBContext.parties.Where(linq => linq.IsDeleted == false).Select
                (linq => new Party
                {
                    PartyId = linq.PartyId,
                    PartyName = linq.PartyName,
                    AdminId = linq.AdminId,
                    Created = linq.Created,
                    IsDeleted = linq.IsDeleted,
                    Modified = linq.Modified,


                }).ToList();
            foreach (var partydata in parties)
            {
                partydata.AdminResponses = Adminresponse(partydata.PartyId);

            }
            if (parties.Count != 0)
            {
                return parties;
            }
            return null;
        }

        public Party UpdateParty(int partyId, PartyRequest partyRequest, int adminId)
        {
            Party party = this.dBContext.parties.FirstOrDefault(linq => linq.PartyId == partyId && linq.IsDeleted == false);
            if (party != null)
            {
                party.AdminId = party.AdminId;
                party.PartyName = partyRequest.PartyName;
                party.Modified = DateTime.Now;
                var partyData = this.dBContext.parties.Attach(party);
                partyData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.dBContext.SaveChanges();
                if (partyData.State != 0)
                {
                    var data = new AdminParty
                    {
                        PartyId = partyId,
                        AdminId = adminId
                    };
                    this.dBContext.adminParties.Add(data);
                    this.dBContext.SaveChanges();
                    party.AdminResponses = Adminresponse(partyId);

                }

                return party;
            }
            return null;
        }
        private int Frequency(List<DeleteList> list1,int value)
        {
            int count = list1.Where(linq => linq.PartyId == value).Count();
            return count;
        }
        private List<AdminResponse> Adminresponse(int partyId)
        {
            List<AdminResponse> adminResponse = dBContext.adminParties.Where(linq => linq.PartyId == partyId).Join
                    (dBContext.admins,
                    admn => admn.AdminId,
                    party => party.AdminId,
                    (party, adm) => new AdminResponse
                    {
                        AdminId = adm.AdminId,
                        AdminFirstName = adm.AdmFirstName,
                        AdminLastName = adm.AdmLastName,
                        AdminContactNumber = adm.AdmContactNumber,
                        AdminEmail = adm.AdmEmail,
                    }
                    ).ToList();
            return adminResponse;
        }
    }
}
