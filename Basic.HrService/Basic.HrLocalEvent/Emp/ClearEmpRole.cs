using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Delete")]
    internal class ClearEmpRole : IEventHandler<EmpLocalEvent>
    {
        private readonly IEmpDeptProwerCollect _Service;

        public ClearEmpRole (IEmpDeptProwerCollect service)
        {
            this._Service = service;
        }

        public void HandleEvent (EmpLocalEvent data, string eventName)
        {
            if (data.Emp.IsOpenAccount)
            {
                this._Service.Clear(data.Emp.EmpId);
            }
        }
    }
}
