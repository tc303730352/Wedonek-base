using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.DeptPower;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class EmpDeptPowerCollect : IEmpDeptPowerCollect
    {
        private readonly IEmpDeptPowerDAL _DeptPower;

        public EmpDeptPowerCollect ( IEmpDeptPowerDAL deptPower )
        {
            this._DeptPower = deptPower;
        }

        public void Clear ( long empId )
        {
            long[] ids = this._DeptPower.Gets(a => a.EmpId == empId, a => a.Id);
            if ( ids.IsNull() )
            {
                return;
            }
            this._DeptPower.Delete(ids);
        }
        public DeptPowerDto[] GetDepts ( long[] empId, long companyId )
        {
            return this._DeptPower.GetDepts(empId, companyId);
        }
        public Dictionary<long, int> GetPowerNum ( long[] empId, long companyId )
        {
            return this._DeptPower.GetPowerNum(empId, companyId);
        }
        public long[] GetDeptId ( long empId, long companyId )
        {
            var list = this._DeptPower.Gets(a => a.EmpId == empId && a.CompanyId == companyId, a => new
            {
                a.DeptId,
                a.UnitId
            });
            List<long> ids = new List<long>(list.Length * 2);
            list.ForEach(c =>
            {
                ids.Add(c.DeptId);
                ids.Add(c.UnitId);
            });
            return ids.Distinct().ToArray();
        }

        public bool SetDeptId ( long empId, long companyId, KeyValuePair<long, long>[] depts )
        {
            var old = this._DeptPower.Gets(a => a.EmpId == empId && a.CompanyId == companyId, a => new
            {
                a.DeptId,
                a.Id
            });
            if ( old.Length == depts.Length && old.IsExists(c => !depts.IsExists(a => a.Key == c.DeptId)) == false )
            {
                return false;
            }
            DBEmpDeptPower[] adds = depts.ConvertAll(c => new DBEmpDeptPower
            {
                DeptId = c.Key,
                UnitId = c.Value,
                CompanyId = companyId,
                EmpId = empId
            });
            this._DeptPower.Set(old.ConvertAll(a => a.Id), adds);
            return true;
        }

        public EmpDeptPower[] GetDeptPower ( long[] empId, long[] deptId )
        {
            return this._DeptPower.Gets<EmpDeptPower>(a => empId.Contains(a.EmpId) && deptId.Contains(a.DeptId));
        }

        public void Add ( DeptPowerAdd add )
        {
            if ( this._DeptPower.IsExists(a => a.EmpId == add.EmpId && a.DeptId == add.DeptId) )
            {
                return;
            }
            this._DeptPower.Add(add);
        }
    }
}
