using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.TableGroup
{
    [IRemoteConfig("basic.form.service")]
    public class AddTableGroup : RpcRemote<long>
    {
        public TableGroupAdd Datum { get; set; }
    }
}
