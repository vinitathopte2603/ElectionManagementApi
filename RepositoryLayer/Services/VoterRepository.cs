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
    public class VoterRepository : IVoterRepository
    {
        readonly DBContext dBContext;
        public VoterRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public bool DeleteVoter(int voterId, int adminId)
        {
            Voter voter = this.dBContext.voters.FirstOrDefault(linq => linq.VoterId == voterId && linq.IsDeleted == false);
            if (voter != null)
            {
                voter.IsDeleted = true;
                voter.Modified = DateTime.Now;
                var voterData = this.dBContext.voters.Attach(voter);
                voterData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.dBContext.SaveChanges();
                if (voterData.State != 0)
                {
                    var data = new AdminVoter
                    {
                        VoterId = voterId,
                        AdminId = adminId
                    };
                    this.dBContext.adminVoters.Add(data);
                    this.dBContext.SaveChanges();
                    voter.AdminResponses = Adminresponse(voterId);

                }

                return true;

            }
            return false;
        }

        public List<Voter> GetAllVoters()
        {
            List<Voter> voters = dBContext.voters.Where(linq => linq.IsDeleted == false).Select
                (linq => new Voter
                {
                    VoterId = linq.VoterId,
                    VoterFirstName = linq.VoterFirstName,
                    VoterLastName = linq.VoterLastName,
                    CandidateId = linq.CandidateId,
                    VoterContactNUmber = linq.VoterContactNUmber,
                    Created = linq.Created,
                    UniqueVoterId = linq.UniqueVoterId

                }).ToList();
            foreach (var voterdata in voters)
            {
                voterdata.AdminResponses = Adminresponse(voterdata.VoterId);

            }
            if (voters.Count != 0)
            {
                return voters;
            }
            return null;
        }

        public Voter UpdateVoter(int voterId, VoterDataRequest voterDataRequest, int adminId)
        {
            Voter voter = this.dBContext.voters.FirstOrDefault(linq => linq.VoterId == voterId && linq.IsDeleted == false);
            if (voter != null)
            {
                voter.VoterFirstName = voterDataRequest.VoterFirstName;
                voter.VoterLastName = voterDataRequest.VoterLastName;
                voter.VoterContactNUmber = voterDataRequest.VoterContactNUmber;
                voter.Modified = DateTime.Now;
                var voterData = this.dBContext.voters.Attach(voter);
                voterData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.dBContext.SaveChanges();
                if (voterData.State != 0)
                {
                    var data = new AdminVoter
                    {
                        VoterId = voterId,
                        AdminId = adminId
                    };
                    this.dBContext.adminVoters.Add(data);
                    this.dBContext.SaveChanges();
                    voter.AdminResponses = Adminresponse(voterId);

                }

                return voter;
            }
            return null;
        }

        public Voter Vote(VoterRequest voterRequest)
        {
            var voterData = this.dBContext.voters.FirstOrDefault(linq => linq.UniqueVoterId == voterRequest.UniqueVoterId);
            Candidate candidate = this.dBContext.candidates.FirstOrDefault(linq => linq.CandidateId == voterRequest.CandidateId);
            if (voterData != null)
            {
                return null;
            }
            else
            {
                Voter voter = new Voter()
                {
                    CandidateId = voterRequest.CandidateId,
                    VoterFirstName = voterRequest.VoterFirstName,
                    VoterLastName = voterRequest.VoterLastName,
                    UniqueVoterId = voterRequest.UniqueVoterId,
                    VoterContactNUmber = voterRequest.VoterContactNUmber
                };
                this.dBContext.voters.Add(voter);
                this.dBContext.SaveChanges();
                candidate.Votes = candidate.Votes + 1;
                this.dBContext.SaveChanges();
                return voter;
            }
        }
        private List<AdminResponse> Adminresponse(int voterId)
        {
            List<AdminResponse> adminResponse = dBContext.adminVoters.Where(linq => linq.VoterId == voterId).Join
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
