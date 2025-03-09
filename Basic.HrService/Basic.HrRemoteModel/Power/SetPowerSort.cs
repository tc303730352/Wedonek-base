using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    [IRemoteConfig("basic.hr.service")]
    public class SetPowerSort : RpcRemote<bool>
    {
        public long Id { get; set; }

        public int Sort { get; set; }
    }
}
