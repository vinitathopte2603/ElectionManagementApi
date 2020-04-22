using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IElectionResultStatusBusiness
    {
        List<ConstituencyWiseResponse> ConstituencyWiseResult(int constituencyId);
        List<PartyWiseResponse> PartyWiseResult(string state);
    }
}
