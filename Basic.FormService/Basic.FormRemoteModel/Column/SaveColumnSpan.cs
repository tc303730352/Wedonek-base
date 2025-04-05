using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class SaveColumnSpan : RpcRemote
    {
        public KeyValuePair<long, int>[] ColSpan { get; set; }
    }
}
