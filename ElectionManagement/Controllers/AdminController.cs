using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ElectionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AdminController : ControllerBase
    {
        readonly IAdminBusiness adminBusiness;
        readonly IConfiguration configuration;
       
        public AdminController(IAdminBusiness adminBusiness,IConfiguration configuration)
        {
            this.adminBusiness = adminBusiness;
            this.configuration = configuration;
           
        }
        [HttpPost]
        [Route("Registration")]
        
        public IActionResult AdminRegistration ([FromBody]AdminRequest adminRequest)
        {
            try
            {
                bool status;
                string message;
                
                    var data = this.adminBusiness.AdminRegistration(adminRequest);
                    if (data != null)
                    {
                        status = true;
                        message = "Admin registered successfully";
                        return this.Ok(new { status, message, data });
                    }
                    status = false;
                    message = "Email already exists";
                    return this.BadRequest(new { status, message });
                     
            }
            catch(Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("Login")]
        
        public IActionResult AdminLogin([FromBody]LoginRequest loginRequest)
        {
            try
            {
                bool status;
                string message;
                var data = this.adminBusiness.AdminLogin(loginRequest);
                if (data != null)
                {
                    status = true;
                    message = "Admin logged in successfully";
                    var token = GetToken(data);
                    return this.Ok(new { status, message, token, data });
                }
                status = false;
                message = "Email or password is incorrect";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        private string GetToken(LoginResponse loginResponse)
        {
            try
            {
                var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim("AdminId", loginResponse.AdminId.ToString()),
                new Claim("Email", loginResponse.AdminEmail)
            };
                var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
                    this.configuration["Jwt:Issuer"],
                     claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
    }
}