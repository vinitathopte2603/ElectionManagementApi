using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;


namespace BusinessLayer.Interfaces
{
    public interface IAdminBusiness
    {
        AdminResponse AdminRegistration(AdminRequest adminRequest);
        LoginResponse AdminLogin(LoginRequest loginRequest);
    }
}
