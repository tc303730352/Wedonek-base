using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.TableGroup
{
    [IRemoteConfig("basic.form.service")]
    public class SetTableGroupSort : RpcRemote
    {
        public KeyValuePair<long, int>[] Sort { get; set; }
    }
}
