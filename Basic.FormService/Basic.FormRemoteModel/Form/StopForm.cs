using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Form
{
    [IRemoteConfig("basic.form.service")]
    public class StopForm : RpcRemote
    {
        public long Id { get; set; }
    }
}
