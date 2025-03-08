using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    [IRemoteConfig("basic.hr.service")]
    public class SetProwerSort : RpcRemote<bool>
    {
        public long Id { get; set; }

        public int Sort { get; set; }
    }
}
