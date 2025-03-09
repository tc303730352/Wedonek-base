using Basic.HrModel.DB;

namespace Basic.HrLocalEvent.Model
{
    public class PowerEvent : WeDonekRpc.Client.EventPublic
    {
        public PowerEvent ( DBPowerList power )
        {
            this.Power = power;
        }
        public DBPowerList Power { get; }
    }
}
