using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRepository
    {
        AdminResponse AdminRegistration(AdminRequest adminRequest);
        LoginResponse AdminLogin(LoginRequest loginRequest);
    }
}
