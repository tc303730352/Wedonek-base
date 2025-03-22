using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class DeptPowerService : IDeptPowerService
    {
        private readonly IEmpDeptPowerCollect _DeptPower;
        private readonly IDeptCollect _Dept;
        public DeptPowerService ( IEmpDeptPowerCollect deptPower, IDeptCollect dept )
        {
            this._Dept = dept;
            this._DeptPower = deptPower;
        }

        public long[] GetDeptPower ( long empId, long companyId )
        {
            return this._DeptPower.GetDeptId(empId, companyId);
        }
        public void SetPower ( long empId, long companyId, long[] deptId )
        {
            KeyValuePair<long, long>[] depts = this._Dept.GetUnitId(deptId);

            if ( this._DeptPower.SetDeptId(empId, companyId, depts) )
            {
                new UserChangeEvent(empId).AsyncSend("DeptPowerChange");
            }
        }
    }
}
