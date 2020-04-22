using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class DeletePartiesRequest
    {
       public List<DeleteList> PartyIds { get; set; }
    }
    public class DeleteList
    {
        public int PartyId { get; set; }
    }
}
