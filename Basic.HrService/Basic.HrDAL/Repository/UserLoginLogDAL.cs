using Basic.HrModel.DB;
using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class UserLoginLogDAL : IUserLoginLogDAL
    {
        private readonly IRepository<DBUserLoginLog> _BasicDAL;
        public UserLoginLogDAL ( IRepository<DBUserLoginLog> basicDAL )
        {
            this._BasicDAL = basicDAL;
        }

        public void Adds ( DBUserLoginLog[] logs )
        {
            this._BasicDAL.Insert(logs);
        }
        public Result[] Query<Result> ( LoginLogQuery query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
    }
}
