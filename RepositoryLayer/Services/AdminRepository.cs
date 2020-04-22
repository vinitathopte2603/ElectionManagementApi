using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ModelContext;
using System;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DBContext dBContext;
        public AdminRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public LoginResponse AdminLogin(LoginRequest loginRequest)
        {
            try
            {
                loginRequest.Password = EncodeDecode.EncodePassword(loginRequest.Password);
                var admin = this.dBContext.admins.FirstOrDefault(linq => linq.AdmEmail == loginRequest.AdminEmail && linq.Password == loginRequest.Password);
                if (admin != null)
                {
                    var adminData = new LoginResponse()
                    {
                        AdminId = admin.AdminId,
                        AdminFirstName = admin.AdmFirstName,
                        AdminLastName = admin.AdmLastName,
                        AdminContactNumber = admin.AdmContactNumber,
                        AdminEmail = admin.AdmEmail
                    };
                    return adminData;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AdminResponse AdminRegistration(AdminRequest adminRequest)
        {
            try
            {
                var data = this.dBContext.admins.FirstOrDefault(linq => linq.AdmEmail == adminRequest.AdminEmail);
                if (data != null)
                {
                    return null;
                }
                adminRequest.Password = EncodeDecode.EncodePassword(adminRequest.Password);
                Admin admin = new Admin()
                {
                    AdmFirstName = adminRequest.AdminFirstName,
                    AdmLastName = adminRequest.AdminLastName,
                    AdmContactNumber = adminRequest.AdminContactNumber,
                    AdmEmail = adminRequest.AdminEmail,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Password = adminRequest.Password
                };
                this.dBContext.admins.Add(admin);
                dBContext.SaveChanges();
                if (adminRequest != null)
                {
                    AdminResponse adminResponse = new AdminResponse()
                    {
                        AdminId = admin.AdminId,
                        AdminFirstName = admin.AdmFirstName,
                        AdminLastName = admin.AdmLastName,
                        AdminContactNumber = admin.AdmContactNumber,
                        AdminEmail = admin.AdmEmail,
                    };
                    return adminResponse;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
