using Basic.HrRemoteModel.LoginUser;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class LoginUserEvent : IRpcApiService
    {
        private readonly ILoginUserService _Service;

        public LoginUserEvent (ILoginUserService service)
        {
            this._Service = service;
        }

        public void DeleteAccount (DeleteAccount obj)
        {
            this._Service.Delete(obj.EmpId);
        }
        public void OpenAccount (OpenAccount obj)
        {
            this._Service.Open(obj.EmpId, obj.CompanyId);
        }
    }
}
