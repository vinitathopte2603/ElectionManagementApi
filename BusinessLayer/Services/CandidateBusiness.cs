using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CandidateBusiness : ICandidateBusiness
    {
        readonly ICandidateRepository candidateRepository;
        public CandidateBusiness(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }
        public Candidate AddCandidate(CandidateRequest candidateRequest, int adminId)
        {
            if(candidateRequest!=null && adminId!=0)
            {
                return this.candidateRepository.AddCandidate(candidateRequest, adminId);
            }
            return null;
        }

        public bool DeleteCandidate(int candidateId, int adminId)
        {
            if (candidateId != 0 && adminId != 0)
            {
                return this.candidateRepository.DeleteCandidate(candidateId, adminId);
            }
            return false;
        }

        public List<CandidateResponse> GetAllCandidates()
        {
            return this.candidateRepository.GetAllCandidates();
        }

        public Candidate UpdateCandidate(int candidateId, CandidateDataRequest candidateDataRequest, int adminId)
        {
            if (candidateDataRequest != null && candidateId != 0 && adminId != 0)
            {
                return this.candidateRepository.UpdateCandidate(candidateId, candidateDataRequest, adminId);
            }
            return null;
        }
    }
}
