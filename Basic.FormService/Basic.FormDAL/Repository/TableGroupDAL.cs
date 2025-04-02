using Basic.FormModel.DB;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class TableGroupDAL : BasicDAL<DBTableGroup, long>
    {
        public TableGroupDAL(IRepository<DBTableGroup> basicDAL) : base(basicDAL)
        {
        }
    }
}
