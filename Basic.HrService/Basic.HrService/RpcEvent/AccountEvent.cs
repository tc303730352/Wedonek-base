using Basic.HrRemoteModel.LoginUser;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class AccountEvent : IRpcApiService
    {
        private readonly IAccountService _Service;

        public AccountEvent (IAccountService service)
        {
            this._Service = service;
        }

        public void ResetUserPwd (ResetUserPwd reset)
        {
            this._Service.ResetPwd(reset.EmpId);
        }

        public void UpdateUserPwd (UpdateUserPwd pwd)
        {
            this._Service.UpdatePwd(pwd.EmpId, pwd.SetArg.NewPwd, pwd.SetArg.Pwd);
        }
    }
}
