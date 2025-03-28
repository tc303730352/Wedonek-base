using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Role
{
    [LocalEventName("Delete")]
    internal class EmpRoleDeleteEvent : IEventHandler<RoleEvent>
    {
        private readonly IEmpRoleCollect _Service;

        public EmpRoleDeleteEvent ( IEmpRoleCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( RoleEvent data, string eventName )
        {
            this._Service.ClearByRoleId(data.Role.Id);
        }
    }
}
