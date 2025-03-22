using Basic.HrRemoteModel.EmpLogin.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpLoginService
    {
        ComSwitchResult Switch ( long empId, long companyId );
        LoginResult Login ( long empId );
        LoginResult PwdLogin ( string username, string password );
    }
}