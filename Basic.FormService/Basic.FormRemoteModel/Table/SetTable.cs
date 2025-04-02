using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Table
{
    [IRemoteConfig("basic.form.service")]
    public class SetTable : RpcRemote<bool>
    {
        public long Id { get; set; }

        public CustomTableSet Datum { get; set; }
    }
}
