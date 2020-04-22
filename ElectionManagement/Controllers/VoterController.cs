using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ModelContext;

namespace ElectionManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        readonly IVoterBusiness voterBusiness;
        readonly DBContext dBContext;
        public VoterController(IVoterBusiness voterBusiness, DBContext dBContext)
        {
            this.voterBusiness = voterBusiness;
            this.dBContext = dBContext;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Vote([FromBody]VoterRequest voterRequest)
        {
            try
            {
                
                string message;
                bool status;
                var voted = this.dBContext.voters.FirstOrDefault(linq => linq.UniqueVoterId == voterRequest.UniqueVoterId);
                if (voted != null)
                {
                    status = false;
                    message = "Vote already casted";
                    return this.BadRequest(new { status, message });
                }
                
                
                    var data = this.voterBusiness.Vote(voterRequest);
                    if (data != null)
                    {
                        status = true;
                        message = "vote registered successfully";
                        return this.Ok(new { status, message, data });
                    }
                

                status = false;
                message = "invalid voter";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{voterId}/Delete")]
        public IActionResult DeleteVoter(int voterId)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    bool data = this.voterBusiness.DeleteVoter(voterId, adminId);
                    if (data)
                    {
                        status = true;
                        message = "voter deleted successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "voter doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllVoters()
        {
            try
            {
                bool status;
                string message;
                List<Voter> data = this.voterBusiness.GetAllVoters();
                if (data.Count != 0)
                {
                    status = true;
                    message = "All pavotersrties";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "voters not present";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("{voterId}")]
        public IActionResult UpdateVoter(int voterId, [FromBody]VoterDataRequest voterDataRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.voterBusiness.UpdateVoter(voterId, voterDataRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "voter updated successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "voter doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

    }
}