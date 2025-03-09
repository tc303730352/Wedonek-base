using Basic.HrModel.DB;
using Basic.HrModel.DeptPower;
using LinqKit;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;
namespace Basic.HrDAL.Repository
{
    internal class EmpDeptPowerDAL : BasicDAL<DBEmpDeptPower, long>, IEmpDeptPowerDAL
    {
        public EmpDeptPowerDAL ( IRepository<DBEmpDeptPower> basicDAL ) : base(basicDAL)
        {
        }
        private void _Add ( DBEmpDeptPower[] adds )
        {
            adds.ForEach(c =>
            {
                c.Id = IdentityHelper.CreateId();
            });
            this._BasicDAL.Insert(adds);
        }

        public DeptPowerDto[] GetDepts ( long[] empId, long companyId )
        {
            return this._BasicDAL.Gets<DeptPowerDto>(a => empId.Contains(a.EmpId) && a.CompanyId == companyId);
        }
        public void Set ( long[] ids, DBEmpDeptPower[] adds )
        {
            if ( ids.IsNull() )
            {
                this._Add(adds);
                return;
            }
            adds.ForEach(c => c.Id = IdentityHelper.CreateId());
            ISqlQueue<DBEmpDeptPower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(adds);
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.emp.dept.power.set.error");
            }
        }

        public void Add ( DeptPowerAdd add )
        {
            this._BasicDAL.Insert(add.ConvertMap<DeptPowerAdd, DBEmpDeptPower>(( a, b ) =>
            {
                b.Id = IdentityHelper.CreateId();
                return b;
            }));
        }
    }
}
