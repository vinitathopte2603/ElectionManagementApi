using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        readonly IPartyBusiness partyBusiness;
        public PartyController(IPartyBusiness partyBusiness)
        {
            this.partyBusiness = partyBusiness;
        }


        [HttpPost]
        public IActionResult AddParty([FromBody]PartyRequest partyRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "Email"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.partyBusiness.AddParty(partyRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "Party added successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "Party already exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{partyId}/Delete")]
        public IActionResult DeleteParty(int partyId)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    bool data = this.partyBusiness.DeleteParty(partyId, adminId);
                    if (data)
                    {
                        status = true;
                        message = "Party deleted successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "Party doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllParties()
        {
            try
            {
                bool status;
                string message;
                List<Party> data = this.partyBusiness.GetAllParties();
                if (data.Count != 0)
                {
                    status = true;
                    message = "All parties";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "Parties not present";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("{partyId}")]
        public IActionResult UpdateParty(int partyId, [FromBody]PartyRequest partyRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.partyBusiness.UpdateParty(partyId, partyRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "Party updated successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "Party doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("Parties/Delete")]
        public IActionResult DeleteParties( DeletePartiesRequest deletePartiesRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    bool data = this.partyBusiness.DeleteParties(deletePartiesRequest, adminId);
                    if (data)
                    {
                        status = true;
                        message = "Parties deleted successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "Parties doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}