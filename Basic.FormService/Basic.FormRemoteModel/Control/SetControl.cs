using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class SetControl : RpcRemote<bool>
    {
        public long Id { get; set; }
        public ControlSet Datum { get; set; }
    }
}
