using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Power
{
    [LocalEventName("Delete")]
    internal class ClearPowerEvent : IEventHandler<PowerEvent>
    {
        private readonly IOperatePowerCollect _Service;

        public ClearPowerEvent ( IOperatePowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( PowerEvent data, string eventName )
        {
            this._Service.Clear(data.Power.Id);
        }
    }
}
