using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        readonly ICandidateBusiness candidateBusiness;
        public CandidateController(ICandidateBusiness candidateBusiness)
        {
            this.candidateBusiness = candidateBusiness;
        }
        [HttpPost]
        public IActionResult AddCandidate([FromBody]CandidateRequest candidateRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "Email"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.candidateBusiness.AddCandidate(candidateRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "Candidate added successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "candidate already exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{candidateId}/Delete")]
        public IActionResult DeleteCandidate(int candidateId)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    bool data = this.candidateBusiness.DeleteCandidate(candidateId, adminId);
                    if (data)
                    {
                        status = true;
                        message = "Candidate deleted successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "candidate doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCandidates()
        {
            try
            {
                bool status;
                string message;
                List<CandidateResponse> data = this.candidateBusiness.GetAllCandidates();
                if (data.Count != 0)
                {
                    status = true;
                    message = "All candidates";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "candidates not present";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("{candidateId}")]
        public IActionResult UpdateCandidate(int candidateId, [FromBody]CandidateDataRequest candidateDataRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.candidateBusiness.UpdateCandidate(candidateId, candidateDataRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "candidate updated successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "candidate doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}