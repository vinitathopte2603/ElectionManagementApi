using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionStatusResultController : ControllerBase
    {
        readonly IElectionResultStatusBusiness electionResultStatusBusiness;

        public ElectionStatusResultController(IElectionResultStatusBusiness electionResultStatusBusiness)
        {
            this.electionResultStatusBusiness = electionResultStatusBusiness;

        }
        [HttpGet]
        [Route("Constituency-Wise")]

        public IActionResult ConstituencyWiseResult(int constituencyId)
        {
            try
            {
                bool status;
                string message;

                var data = this.electionResultStatusBusiness.ConstituencyWiseResult(constituencyId);
                if (data != null)
                {
                    status = true;
                    message = "constituency wise result";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "constituency does not exist";
                return this.BadRequest(new { status, message });

            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("Party-Wise")]

        public IActionResult PartyWiseResult(string state)
        {
            try
            {
                bool status;
                string message;
                var data = this.electionResultStatusBusiness.PartyWiseResult(state);
                if (data != null)
                {
                    status = true;
                    message = "Partywise result";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "Party does not exist";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}