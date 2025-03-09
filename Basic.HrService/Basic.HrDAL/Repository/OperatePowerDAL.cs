using Basic.HrModel.DB;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class OperatePowerDAL : BasicDAL<DBOperatePower, long>, IOperatePowerDAL
    {
        public OperatePowerDAL ( IRepository<DBOperatePower> basicDAL ) : base(basicDAL)
        {
        }
        public long Add ( DBOperatePower add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        public void SetIsEnable ( DBOperatePower source, bool isEnable )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == isEnable, a => a.Id == source.Id) )
            {
                throw new ErrorException("hr.operate.power.set.fail");
            }
        }
    }
}
