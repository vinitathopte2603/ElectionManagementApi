using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IElectionResultStatusRepository
    {
        List<ConstituencyWiseResponse> ConstituencyWiseResult(int constituencyId);
        List<PartyWiseResponse> PartyWiseResult(string state);

    }
}
