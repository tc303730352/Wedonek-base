using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class UserOperateLogDAL : BasicDAL<DBUserOperateLog, long>, IUserOperateLogDAL
    {
        public UserOperateLogDAL ( IRepository<DBUserOperateLog> basicDAL ) : base(basicDAL)
        {
        }
        public void Adds ( DBUserOperateLog[] logs )
        {
            this._BasicDAL.Insert(logs);
        }
        public Result[] Query<Result> ( OpLogQueryParam query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
    }
}
