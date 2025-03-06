using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.User
{
    [LocalEventName("Delete")]
    internal class ClearEmpDeptPrower : IEventHandler<UserChangeEvent>
    {
        private readonly IEmpDeptProwerCollect _EmpDeptPrower;

        public ClearEmpDeptPrower (IEmpDeptProwerCollect empDeptPrower)
        {
            this._EmpDeptPrower = empDeptPrower;
        }

        public void HandleEvent (UserChangeEvent data, string eventName)
        {
            this._EmpDeptPrower.Clear(data.EmpId);
        }
    }
}
