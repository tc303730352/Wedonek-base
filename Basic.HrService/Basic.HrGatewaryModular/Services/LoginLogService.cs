using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.LoginLog;
using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class LoginLogService : ILoginLogService
    {
        public PagingResult<UserLoginLog> Query ( PagingParam<LoginLogQuery> query )
        {
            return new QueryLoginLog
            {
                Query = query.Query,
                Index = query.Index,
                NextId = query.NextId,
                Size = query.Size,
                IsDesc = query.IsDesc,
                SortName = query.SortName,
            }.Send();
        }
    }
}
