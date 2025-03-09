using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Role
{
    [LocalEventName("Delete")]
    internal class RoleDeleteEvent : IEventHandler<RoleEvent>
    {
        private readonly IRoleOperatePowerCollect _Service;

        public RoleDeleteEvent ( IRoleOperatePowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( RoleEvent data, string eventName )
        {
            this._Service.Clear(data.Role.Id);
        }
    }
}
