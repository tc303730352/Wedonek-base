using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Column
{
    [IRemoteConfig("basic.form.service")]
    public class SetColumnGroupId : RpcRemote<bool>
    {
        public long Id { get; set; }

        public long GroupId { get; set; }
    }
}
