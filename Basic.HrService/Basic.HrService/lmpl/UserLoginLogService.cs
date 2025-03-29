using Basic.HrCollect;
using Basic.HrRemoteModel.LoginLog.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class UserLoginLogService : IUserLoginLogService
    {
        private readonly IUserLoginLogCollect _LoginLog;

        public UserLoginLogService ( IUserLoginLogCollect loginLog )
        {
            this._LoginLog = loginLog;
        }

        public PagingResult<UserLoginLog> Query ( LoginLogQuery query, IBasicPage paging )
        {
            UserLoginLog[] logs = this._LoginLog.Query<UserLoginLog>(query, paging, out int count);
            return new PagingResult<UserLoginLog>(logs, count);
        }
    }
}
