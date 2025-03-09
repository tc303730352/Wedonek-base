using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrModel.DeptPower;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Update")]
    internal class SetEmpDeptPower : IEventHandler<EmpLocalEvent>
    {
        private readonly IEmpDeptPowerCollect _Service;

        public SetEmpDeptPower ( IEmpDeptPowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( EmpLocalEvent data, string eventName )
        {
            if ( data.Emp.IsOpenAccount && data.UpdateCol != null && data.UpdateCol.Contains("DeptId") )
            {
                this._Service.Add(new DeptPowerAdd
                {
                    CompanyId = data.Emp.CompanyId,
                    DeptId = data.Emp.DeptId,
                    EmpId = data.Emp.EmpId,
                    UnitId = data.Emp.UnitId
                });
            }
        }
    }
}
