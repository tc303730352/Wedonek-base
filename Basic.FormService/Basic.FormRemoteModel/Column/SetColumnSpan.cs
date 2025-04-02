using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class SetColumnSpan : RpcRemote<bool>
    {
        public long Id { get; set; }

        public int ColSpan { get; set; }
    }
}
