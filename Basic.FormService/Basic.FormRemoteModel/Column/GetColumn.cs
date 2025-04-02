using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class GetColumn : RpcRemote<TableColumnDto>
    {
        public long Id { get; set; }
    }
}
