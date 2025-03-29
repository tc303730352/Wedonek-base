using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 登陆日志
    /// </summary>
    internal class LoginLogApi : ApiController
    {
        private readonly ILoginLogService _Service;

        public LoginLogApi ( ILoginLogService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 查询用户登陆日志
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        public PagingResult<UserLoginLog> Query ( PagingParam<LoginLogQuery> query )
        {
            return this._Service.Query(query);
        }
    }
}
