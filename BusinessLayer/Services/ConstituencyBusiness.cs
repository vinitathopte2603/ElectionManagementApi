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
    public class ConstituencyBusiness : IConstituencyBusiness
    {
        readonly IConstituencyRepository constituencyRepository;
        public ConstituencyBusiness (IConstituencyRepository constituencyRepository)
        {
            this.constituencyRepository = constituencyRepository;
        }
        public Constituency AddConstituency(ConstituencyRequest constituencyRequest, int adminId)
        {
            try
            {
                if (constituencyRequest != null && adminId != 0)
                {
                    return this.constituencyRepository.AddConstituency(constituencyRequest, adminId);
                }
                return null;
            }
           catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteConstituency(int constituencyId, int adminId)
        {
            try
            {
                if (constituencyId != 0)
                {
                    return this.constituencyRepository.DeleteConstituency(constituencyId, adminId);
                }
                return false;
            }
           catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Constituency> GetAllConstituencies( string state)
        {
            if (state != null)
            {
                return this.constituencyRepository.GetAllConstituencies(state);
            }
            return null;
        }

        public List<NameResponse> GetAllStates()
        {
            return this.constituencyRepository.GetAllStates();
        }

        public Constituency UpdateConstituency(int constituencyId, ConstituencyDataRequest constituencyDataRequest, int adminId)
        {
            try
            {
                if (constituencyId != 0 && constituencyDataRequest != null && adminId != 0)
                {
                    return this.constituencyRepository.UpdateConstituency(constituencyId, constituencyDataRequest, adminId);
                }
                return null;
            }
          catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
