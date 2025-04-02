using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class DeleteColumn : RpcRemote
    {
        public long Id { get; set; }
    }
}
