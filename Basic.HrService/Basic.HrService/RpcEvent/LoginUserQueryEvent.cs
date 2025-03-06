using Basic.HrRemoteModel.LoginUser;
using Basic.HrRemoteModel.LoginUser.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class LoginUserQueryEvent : IRpcApiService
    {
        private readonly ILoginUserQueryService _Service;

        public LoginUserQueryEvent (ILoginUserQueryService service)
        {
            this._Service = service;
        }

        public PagingResult<LoginUserDatum> QueryLoginUser (QueryLoginUser obj)
        {
            return this._Service.Query(obj.Query, obj.ToBasicPage());
        }
    }
}
