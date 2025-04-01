using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class SetControlStatus : RpcRemote<bool>
    {
        public long Id { get; set; }

        public ControlStatus Status { get; set; }
    }
}
