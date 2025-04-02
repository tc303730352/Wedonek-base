using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.TableGroup
{
    [IRemoteConfig("basic.form.service")]
    public class SetTableGroup : RpcRemote<bool>
    {
        public long Id { get; set; }

        public TableGroupSet Datum { get; set; }
    }
}
