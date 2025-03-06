using Basic.HrRemoteModel.EmpLogin.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpLoginService
    {
        LoginResult Login (long empId);
        LoginResult PwdLogin (string username, string password);
    }
}