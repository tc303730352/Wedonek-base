using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.User
{
    [LocalEventName("Delete")]
    internal class ClearEmpDeptPower : IEventHandler<UserChangeEvent>
    {
        private readonly IEmpDeptPowerCollect _EmpDeptPower;

        public ClearEmpDeptPower ( IEmpDeptPowerCollect empDeptPower )
        {
            this._EmpDeptPower = empDeptPower;
        }

        public void HandleEvent ( UserChangeEvent data, string eventName )
        {
            this._EmpDeptPower.Clear(data.EmpId);
        }
    }
}
