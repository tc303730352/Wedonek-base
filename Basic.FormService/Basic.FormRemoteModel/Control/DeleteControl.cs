using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class DeleteControl : RpcRemote
    {
        public long Id { get; set; }
    }
}
