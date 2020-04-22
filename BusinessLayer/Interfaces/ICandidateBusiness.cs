﻿using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICandidateBusiness
    {
        Candidate AddCandidate(CandidateRequest candidateRequest, int adminId);

        bool DeleteCandidate(int candidateId, int adminId);

        Candidate UpdateCandidate(int candidateId, CandidateDataRequest candidateDataRequest, int adminId);

        List<CandidateResponse> GetAllCandidates();
    }
}
