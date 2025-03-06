using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Delete")]
    internal class ClearEmpTitle : IEventHandler<EmpLocalEvent>
    {
        private readonly IEmpTitleCollect _EmpTitle;

        public ClearEmpTitle (IEmpTitleCollect empTitle)
        {
            this._EmpTitle = empTitle;
        }

        public void HandleEvent (EmpLocalEvent data, string eventName)
        {
            this._EmpTitle.Clear(data.Emp.EmpId);
        }
    }
}
