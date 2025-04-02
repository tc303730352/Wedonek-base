using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class SetColumnSort : RpcRemote
    {
        public KeyValuePair<long, int>[] Sort { get; set; }
    }
}

