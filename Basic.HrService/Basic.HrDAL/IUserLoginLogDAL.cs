using Basic.HrModel.DB;
using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IUserLoginLogDAL
    {
        void Adds ( DBUserLoginLog[] logs );
        Result[] Query<Result> ( LoginLogQuery query, IBasicPage paging, out int count ) where Result : class;
    }
}