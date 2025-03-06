using Basic.HrModel.DB;
using Basic.HrModel.DeptPrower;
using LinqKit;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;
namespace Basic.HrDAL.Repository
{
    internal class EmpDeptProwerDAL : BasicDAL<DBEmpDeptPrower, long>, IEmpDeptProwerDAL
    {
        public EmpDeptProwerDAL (IRepository<DBEmpDeptPrower> basicDAL) : base(basicDAL)
        {
        }
        private void _Add (DBEmpDeptPrower[] adds)
        {
            adds.ForEach(c =>
            {
                c.Id = IdentityHelper.CreateId();
            });
            this._BasicDAL.Insert(adds);
        }

        public DeptProwerDto[] GetDepts (long[] empId, long companyId)
        {
            return this._BasicDAL.Gets<DeptProwerDto>(a => empId.Contains(a.EmpId) && a.CompanyId == companyId);
        }
        public void Set (long[] ids, DBEmpDeptPrower[] adds)
        {
            if (ids.IsNull())
            {
                this._Add(adds);
                return;
            }
            adds.ForEach(c => c.Id = IdentityHelper.CreateId());
            ISqlQueue<DBEmpDeptPrower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(adds);
            if (queue.Submit() <= 0)
            {
                throw new ErrorException("hr.emp.dept.prower.set.error");
            }
        }

        public void Add (DeptProwerAdd add)
        {
            this._BasicDAL.Insert(add.ConvertMap<DeptProwerAdd, DBEmpDeptPrower>((a, b) =>
            {
                b.Id = IdentityHelper.CreateId();
                return b;
            }));
        }
    }
}
