using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Update")]
    internal class SetEmpDeptPrower : IEventHandler<EmpLocalEvent>
    {
        private readonly IEmpDeptProwerCollect _Service;

        public SetEmpDeptPrower (IEmpDeptProwerCollect service)
        {
            this._Service = service;
        }

        public void HandleEvent (EmpLocalEvent data, string eventName)
        {
            if (data.Emp.IsOpenAccount && data.UpdateCol != null && data.UpdateCol.Contains("DeptId"))
            {
                this._Service.Add(new HrModel.DeptPrower.DeptProwerAdd
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
