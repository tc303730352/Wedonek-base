using Basic.HrRemoteModel.LoginLog;
using Basic.HrRemoteModel.LoginLog.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class UserLoginLogEvent : IRpcApiService
    {
        private readonly IUserLoginLogService _Service;

        public UserLoginLogEvent ( IUserLoginLogService service )
        {
            this._Service = service;
        }
        public PagingResult<UserLoginLog> QueryLoginLog ( QueryLoginLog query )
        {
            return this._Service.Query(query.Query, query.ToBasicPage());
        }
    }
}
