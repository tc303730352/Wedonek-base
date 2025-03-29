using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IUserOperateLogDAL : IBasicDAL<DBUserOperateLog, long>
    {
        Result[] Query<Result> ( OpLogQueryParam query, IBasicPage paging, out int count ) where Result : class;
        void Adds ( DBUserOperateLog[] logs );

    }
}