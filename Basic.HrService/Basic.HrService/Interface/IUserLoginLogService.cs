using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IUserLoginLogService
    {
        PagingResult<UserLoginLog> Query ( LoginLogQuery query, IBasicPage paging );
    }
}