using Basic.HrModel.DB;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class OperateProwerDAL : BasicDAL<DBOperatePrower, long>, IOperateProwerDAL
    {
        public OperateProwerDAL ( IRepository<DBOperatePrower> basicDAL ) : base(basicDAL)
        {
        }
        public long Add ( DBOperatePrower add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public void SetIsEnable ( DBOperatePrower source, bool isEnable )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == isEnable, a => a.Id == source.Id) )
            {
                throw new ErrorException("hr.operate.prower.set.fail");
            }
        }
    }
}
