using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IOperateLogCollect
    {
        DBUserOperateLog Get ( long id );
        Result[] Query<Result> ( OpLogQueryParam query, IBasicPage paging, out int count ) where Result : class;
    }
}