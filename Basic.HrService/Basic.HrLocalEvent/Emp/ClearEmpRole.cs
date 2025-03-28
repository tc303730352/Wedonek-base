using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Emp
{
    internal class ClearEmpRole : IEventHandler<EmpEntryEvent>
    {
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IEmpTitleCollect _EmpTitle;
        private readonly IEmpDeptPowerCollect _DeptPower;

        public ClearEmpRole ( IEmpRoleCollect empRole,
            IEmpTitleCollect empTitle,
            IEmpDeptPowerCollect deptPower )
        {
            this._EmpRole = empRole;
            this._EmpTitle = empTitle;
            this._DeptPower = deptPower;
        }

        public void HandleEvent ( EmpEntryEvent data, string eventName )
        {
            if ( data.Emp.IsOpenAccount && !this._EmpTitle.CheckIsExists(data.Emp.EmpId, data.CompanyId) )
            {
                this._EmpRole.Clear(data.CompanyId, data.Emp.EmpId);
                this._DeptPower.Clear(data.CompanyId, data.Emp.EmpId);
            }
        }
    }
}
