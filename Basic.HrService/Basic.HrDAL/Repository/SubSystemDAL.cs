using Basic.HrModel.DB;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class SubSystemDAL : BasicDAL<DBSubSystem, long>, ISubSystemDAL
    {
        public SubSystemDAL ( IRepository<DBSubSystem> basicDAL ) : base(basicDAL)
        {
        }
    }
}
