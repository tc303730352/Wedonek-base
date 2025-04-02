using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Table
{
    [IRemoteConfig("basic.form.service")]
    public class DeleteTable : RpcRemote
    {
        public long Id { get; set; }
    }
}
