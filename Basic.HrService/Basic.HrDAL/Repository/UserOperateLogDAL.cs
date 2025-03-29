using Basic.HrModel.DB;
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
    }
}
