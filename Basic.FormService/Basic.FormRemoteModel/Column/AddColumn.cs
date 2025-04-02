using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class AddColumn : RpcRemote<long>
    {
        public TableColumnAdd Datum { get; set; }
    }
}
