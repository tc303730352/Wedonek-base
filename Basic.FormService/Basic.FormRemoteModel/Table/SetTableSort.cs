using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Table
{
    [IRemoteConfig("basic.form.service")]
    public class SetTableSort : RpcRemote
    {
        public KeyValuePair<long, int>[] Sort { get; set; }
    }
}
