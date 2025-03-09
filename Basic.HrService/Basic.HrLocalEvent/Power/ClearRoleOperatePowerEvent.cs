using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Power
{
    [LocalEventName("Delete")]
    internal class ClearRoleOperatePowerEvent : IEventHandler<PowerEvent>
    {
        private readonly IRoleOperatePowerCollect _Service;

        public ClearRoleOperatePowerEvent ( IRoleOperatePowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( PowerEvent data, string eventName )
        {
            this._Service.ClearByPowerId(data.Power.Id);
        }
    }
}
