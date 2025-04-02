using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Table
{
    [IRemoteConfig("basic.form.service")]
    public class AddTable : RpcRemote<long>
    {
        public CustomTableAdd Datum { get; set; }
    }
}
