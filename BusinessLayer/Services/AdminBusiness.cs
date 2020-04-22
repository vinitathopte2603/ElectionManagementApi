using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class AdminBusiness : IAdminBusiness
    {
        readonly IAdminRepository adminRepository;
        public AdminBusiness(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public LoginResponse AdminLogin(LoginRequest loginRequest)
        {
            if(loginRequest!=null)
            {
                return this.adminRepository.AdminLogin(loginRequest);
            }
            return null;
        }

        public AdminResponse AdminRegistration(AdminRequest adminRequest)
        {
            if (adminRequest != null)
            {
                return this.adminRepository.AdminRegistration(adminRequest);
            }
            return null;
        }
    }
}
