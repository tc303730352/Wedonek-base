using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;

namespace Basic.HrLocalEvent.Power
{
    [LocalEventName("Delete")]
    internal class ClearCompanyPower : IEventHandler<PowerEvent>
    {
        private readonly ICompanyPowerCollect _Service;

        public ClearCompanyPower ( ICompanyPowerCollect service )
        {
            this._Service = service;
        }

        public void HandleEvent ( PowerEvent data, string eventName )
        {
            this._Service.ClearByPower(data.Power.Id);
        }
    }
}
