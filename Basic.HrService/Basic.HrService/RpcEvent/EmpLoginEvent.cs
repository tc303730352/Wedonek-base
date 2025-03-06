using Basic.HrRemoteModel.EmpLogin;
using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class EmpLoginEvent : IRpcApiService
    {
        private readonly IEmpLoginService _Service;

        public EmpLoginEvent (IEmpLoginService service)
        {
            this._Service = service;
        }
        public LoginResult EmpLogin (EmpLogin obj)
        {
            return this._Service.Login(obj.EmpId);
        }
        public LoginResult EmpPwdLogin (EmpPwdLogin obj)
        {
            return this._Service.PwdLogin(obj.LoginName, obj.Password);
        }
    }
}
