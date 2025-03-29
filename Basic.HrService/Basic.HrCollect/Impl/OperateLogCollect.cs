using Basic.HrDAL;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class OperateLogCollect : IOperateLogCollect
    {
        private readonly IUserOperateLogDAL _OpLog;

        public OperateLogCollect ( IUserOperateLogDAL operateLog )
        {
            this._OpLog = operateLog;
        }

        public Result[] Query<Result> ( OpLogQueryParam query, IBasicPage paging, out int count ) where Result : class
        {
            return this._OpLog.Query<Result>(query, paging, out count);
        }
    }
}
