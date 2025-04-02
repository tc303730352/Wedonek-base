using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.TableGroup
{
    [IRemoteConfig("basic.form.service")]
    public class DeleteTableGroup : RpcRemote
    {
        public long Id { get; set; }
    }
}
