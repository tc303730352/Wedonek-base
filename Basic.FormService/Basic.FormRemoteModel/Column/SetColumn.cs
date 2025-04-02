using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class SetColumn : RpcRemote<bool>
    {
        public long Id { get; set; }

        public TableColumnSet Datum { get; set; }
    }
}
