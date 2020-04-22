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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ConstituencyController : ControllerBase
    {
        readonly IConstituencyBusiness constituencyBusiness;
        public ConstituencyController(IConstituencyBusiness constituencyBusiness)
        {
            this.constituencyBusiness = constituencyBusiness;
        }


        [HttpPost]
        public IActionResult AddConstituency([FromBody]ConstituencyRequest constituencyRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "Email"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.constituencyBusiness.AddConstituency(constituencyRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "Constituency added successfully";
                        return this.Ok(new { status, message, data });
                    }
               }
                
                status = false;
                message = "constituency already exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{constituencyId}/Delete")]
        public IActionResult DeleteConstituency(int constituencyId)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    bool data = this.constituencyBusiness.DeleteConstituency(constituencyId, adminId);
                    if (data)
                    {
                        status = true;
                        message = "Constituency deleted successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "constituency doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllConstituencies(string state)
        {
            try
            {
                bool status;
                string message;
                if(state==null)
                {
                    state = " ";
                }
                List<Constituency> data = this.constituencyBusiness.GetAllConstituencies(state);
                if (data.Count != 0)
                {
                    status = true;
                    message = "All constituencies";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "Constituencies not present";
                return this.BadRequest(new { status, message });
            }
          catch(Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("{constituencyId}")]
        public IActionResult UpdateConstituency(int constituencyId, [FromBody]ConstituencyDataRequest constituencyDataRequest)
        {
            try
            {
                var user = HttpContext.User;
                string message;
                bool status;
                if (user.HasClaim(linq => linq.Type == "AdminId"))
                {
                    int adminId = Convert.ToInt32(user.Claims.FirstOrDefault(linq => linq.Type == "AdminId").Value);
                    var data = this.constituencyBusiness.UpdateConstituency(constituencyId, constituencyDataRequest, adminId);
                    if (data != null)
                    {
                        status = true;
                        message = "Constituency updated successfully";
                        return this.Ok(new { status, message, data });
                    }
                }

                status = false;
                message = "constituency doesn't exists";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("states")]
        [AllowAnonymous]
        public IActionResult GetAllStates()
        {
            try
            {
                bool status;
                string message;
                var data = this.constituencyBusiness.GetAllStates();
                if (data.Count != 0)
                {
                    status = true;
                    message = "All states";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "states not present";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}