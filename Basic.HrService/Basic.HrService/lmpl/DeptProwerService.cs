using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class DeptProwerService : IDeptProwerService
    {
        private readonly IEmpDeptProwerCollect _DeptPrower;
        private readonly IDeptCollect _Dept;
        public DeptProwerService (IEmpDeptProwerCollect deptPrower, IDeptCollect dept)
        {
            this._Dept = dept;
            this._DeptPrower = deptPrower;
        }

        public long[] GetDeptPrower (long empId, long companyId)
        {
            return this._DeptPrower.GetDeptId(empId, companyId);
        }
        public void SetPrower (long empId, long companyId, long[] deptId)
        {
            KeyValuePair<long, long>[] depts = this._Dept.GetUnitId(deptId);
            if (this._DeptPrower.SetDeptId(empId, companyId, depts))
            {
                new UserChangeEvent(empId).AsyncSend("DeptProwerChange");
            }
        }
    }
}
