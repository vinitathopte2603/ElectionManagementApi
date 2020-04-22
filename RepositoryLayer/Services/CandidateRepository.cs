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
    public class CandidateRepository: ICandidateRepository
    {
        private readonly DBContext dBContext;
        public CandidateRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Candidate AddCandidate(CandidateRequest candidateRequest, int adminId)
        {
            try
            {
                var candidateData = this.dBContext.candidates.FirstOrDefault(linq => linq.CandidateFirstName == candidateRequest.CandidateFirstName);
                if (candidateData == null)
                {
                    Candidate candidate = new Candidate()
                    {
                        CandidateFirstName = candidateRequest.CandidateFirstName,
                        CandidateLastName = candidateRequest.CandidateLastName,
                        CandidatePhoneNumber = candidateRequest.CandidatePhoneNumber,
                        ConstituencyId = candidateRequest.ConstituencyId,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PartyId = candidateRequest.PartyId,
                        AdminId = adminId
                    };
                    this.dBContext.candidates.Add(candidate);
                    this.dBContext.SaveChanges();
                    return candidate;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteCandidate(int candidateId, int adminId)
        {
            try
            {
                var candidate = this.dBContext.candidates.FirstOrDefault(linq => linq.CandidateId == candidateId);
                if (candidate != null)
                {
                  
                    candidate.AdminId = adminId;
                    candidate.Modified = DateTime.Now;
                    candidate.IsDeleted = true;
                    var candidateData = this.dBContext.candidates.Attach(candidate);
                    candidateData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this.dBContext.SaveChanges();
                    if (candidateData.State != 0)
                    {
                        var data = new AdminCandidate
                        {
                            CandidateId = candidateId,
                            AdminId = adminId

                        };
                        this.dBContext.adminCandidates.Add(data);
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

        public List<CandidateResponse> GetAllCandidates()
        {
            List<CandidateResponse> candidatesList = dBContext.candidates.Where(linq => linq.IsDeleted == false).Select
             (linq => new CandidateResponse
             {
                 CandidateId = linq.CandidateId,
                 CandidateFirstName = linq.CandidateFirstName,
                 CandidateLastName = linq.CandidateLastName,
                 ConstituencyId = linq.ConstituencyId,
                 PartyId = linq.PartyId,
                 Votes = linq.Votes,
                 CandidatePhoneNumber = linq.CandidatePhoneNumber,
                 
             }).ToList();
            foreach (var candidatedata in candidatesList)
            {
                candidatedata.AdminResponses = Adminresponse(candidatedata.CandidateId);
                candidatedata.ConstituencyName = NameofConstituency(candidatedata.ConstituencyId, candidatedata.CandidateId);
                candidatedata.PartyName = NameofParty(candidatedata.PartyId, candidatedata.CandidateId);
            }
           
            if (candidatesList.Count != 0)
            {
                return candidatesList;
            }
            return null;
        }

        public Candidate UpdateCandidate(int candidateId, CandidateDataRequest candidateDataRequest, int adminId)
        {

            Candidate candidate = this.dBContext.candidates.FirstOrDefault(linq => linq.CandidateId == candidateId && linq.IsDeleted == false);
            if (candidate != null)
            {
                candidate.CandidateFirstName = candidateDataRequest.CandidateFirstName;
                candidate.CandidateLastName = candidateDataRequest.CandidateLastName;
                candidate.PartyId = candidateDataRequest.PartyId;
                candidate.CandidatePhoneNumber = candidateDataRequest.CandidatePhoneNumber;
                candidate.ConstituencyId = candidateDataRequest.ConstituencyId;
                candidate.Modified = DateTime.Now;
                var candidateData = this.dBContext.candidates.Attach(candidate);
                candidateData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.dBContext.SaveChanges();
                if (candidateData.State != 0)
                {
                    var data = new AdminCandidate
                    {
                        CandidateId = candidateId,
                        AdminId = adminId

                    };
                    this.dBContext.adminCandidates.Add(data);
                    this.dBContext.SaveChanges();
                    candidate.AdminResponses = Adminresponse(candidateId);

                }

                return candidate;
            }
            return null;
        }
        private List<AdminResponse> Adminresponse(int candidateId)
        {
            List<AdminResponse> adminResponse = dBContext.adminCandidates.Where(linq => linq.CandidateId == candidateId).Join
                    (dBContext.admins,
                    admn => admn.AdminId,
                    consi => consi.AdminId,
                    (consi, adm) => new AdminResponse
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
        private List<NameResponse> NameofConstituency(int constituencyId, int candidateId)
        {
            var name = dBContext.candidates.Where(linq => linq.ConstituencyId == constituencyId && linq.CandidateId == candidateId).Join
                (
                dBContext.constituencies,
                prim => prim.ConstituencyId,
                sec => sec.ConstituencyId,
                (prim, sec) => new NameResponse
                {
                    Name = sec.ConstituencyName
                }).ToList();
            return name;
        }
        private List<NameResponse> NameofParty(int partyId, int candidateId)
        {
            var name = dBContext.candidates.Where(linq => linq.PartyId == partyId && linq.CandidateId == candidateId).Join
              (
              dBContext.parties,
              prim => prim.PartyId,
              sec => sec.PartyId,
              (prim, sec) => new NameResponse
              {
                  Name = sec.PartyName
              }).ToList();
            return name;
        }
    }
}
