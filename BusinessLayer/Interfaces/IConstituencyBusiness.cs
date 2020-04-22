using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IConstituencyBusiness
    {
        Constituency AddConstituency(ConstituencyRequest constituencyRequest, int adminId);
        bool DeleteConstituency(int constituencyId, int adminId);
        Constituency UpdateConstituency(int constituencyId, ConstituencyDataRequest constituencyDataRequest, int adminId);
        List<Constituency> GetAllConstituencies(string state);
        List<NameResponse> GetAllStates();
    }
}
