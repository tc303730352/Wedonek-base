using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.TableGroup
{
    [IRemoteConfig("basic.form.service")]
    public class GetTableGroup : RpcRemote<TableGroupDto>
    {
        public long Id { get; set; }
    }
}
