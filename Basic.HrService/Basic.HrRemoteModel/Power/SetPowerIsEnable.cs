using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    [IRemoteConfig("basic.hr.service")]
    public class SetPowerIsEnable : RpcRemote
    {
        public long Id { get; set; }

        public bool IsEnable { get; set; }
    }
}
