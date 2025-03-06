using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.DeptPrower;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class EmpDeptProwerCollect : IEmpDeptProwerCollect
    {
        private readonly IEmpDeptProwerDAL _DeptPrower;

        public EmpDeptProwerCollect (IEmpDeptProwerDAL deptPrower)
        {
            this._DeptPrower = deptPrower;
        }

        public void Clear (long empId)
        {
            long[] ids = this._DeptPrower.Gets(a => a.EmpId == empId, a => a.Id);
            if (ids.IsNull())
            {
                return;
            }
            this._DeptPrower.Delete(ids);
        }
        public DeptProwerDto[] GetDepts (long[] empId, long companyId)
        {
            return this._DeptPrower.GetDepts(empId, companyId);
        }
        public long[] GetDeptId (long empId, long companyId)
        {
            return this._DeptPrower.Gets(a => a.EmpId == empId && a.CompanyId == companyId, a => a.DeptId);
        }

        public bool SetDeptId (long empId, long companyId, KeyValuePair<long, long>[] depts)
        {
            var old = this._DeptPrower.Gets(a => a.EmpId == empId && a.CompanyId == companyId, a => new
            {
                a.DeptId,
                a.Id
            });
            if (old.Length == depts.Length && old.IsExists(c => !depts.IsExists(a => a.Key == c.DeptId)) == false)
            {
                return false;
            }
            DBEmpDeptPrower[] adds = depts.ConvertAll(c => new DBEmpDeptPrower
            {
                DeptId = c.Key,
                UnitId = c.Value,
                CompanyId = companyId,
                EmpId = empId
            });
            this._DeptPrower.Set(old.ConvertAll(a => a.Id), adds);
            return true;
        }

        public EmpDeptPrower[] GetDeptPrower (long[] empId, long[] deptId)
        {
            return this._DeptPrower.Gets<EmpDeptPrower>(a => empId.Contains(a.EmpId) && deptId.Contains(a.DeptId));
        }

        public void Add (DeptProwerAdd add)
        {
            if (this._DeptPrower.IsExists(a => a.EmpId == add.EmpId && a.DeptId == add.DeptId))
            {
                return;
            }
            this._DeptPrower.Add(add);
        }
    }
}
