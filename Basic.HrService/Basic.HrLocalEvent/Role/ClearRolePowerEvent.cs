using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrLocalEvent.Role
{
    [LocalEventName("Update")]
    internal class ClearRolePowerEvent : IEventHandler<RoleEvent>
    {
        private readonly IRoleOperatePowerCollect _Service;

        public ClearRolePowerEvent ( IRoleOperatePowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( RoleEvent data, string eventName )
        {
            if ( data.PowerId.IsNull() )
            {
                this._Service.Clear(data.Role.Id);
                return;
            }
            this._Service.Clear(data.Role.Id, data.PowerId);
        }
    }
}
