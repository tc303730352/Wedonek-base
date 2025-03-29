using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IUserLoginLogCollect
    {
        Result[] Query<Result> ( LoginLogQuery query, IBasicPage paging, out int count ) where Result : class;
    }
}