using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IOperateLogCollect
    {
        Result[] Query<Result> ( OpLogQueryParam query, IBasicPage paging, out int count ) where Result : class;
    }
}