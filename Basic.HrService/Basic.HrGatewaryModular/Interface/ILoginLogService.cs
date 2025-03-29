using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface ILoginLogService
    {
        PagingResult<UserLoginLog> Query ( PagingParam<LoginLogQuery> query );
    }
}