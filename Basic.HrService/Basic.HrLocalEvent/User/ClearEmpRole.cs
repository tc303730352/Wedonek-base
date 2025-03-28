using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.User
{
    [LocalEventName("Delete")]
    internal class ClearEmpRole : IEventHandler<UserChangeEvent>
    {
        private readonly IEmpRoleCollect _EmpRole;

        public ClearEmpRole ( IEmpRoleCollect empRole )
        {
            this._EmpRole = empRole;
        }

        public void HandleEvent ( UserChangeEvent data, string eventName )
        {
            this._EmpRole.Clear(data.EmpId);
        }
    }
}
