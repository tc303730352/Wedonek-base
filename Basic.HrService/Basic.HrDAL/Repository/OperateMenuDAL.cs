using Basic.HrModel.DB;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class OperateMenuDAL : BasicDAL<DBOperateMenu, long>, IOperateMenuDAL
    {
        public OperateMenuDAL ( IRepository<DBOperateMenu> basicDAL ) : base(basicDAL)
        {
        }

        public long Add ( DBOperateMenu add )
        {
            add.Id = IdentityHelper.CreateId();
            add.IsEnable = true;
            this._BasicDAL.Insert(add);
            return add.Id;
        }
    }
}
