using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class GetControl : RpcRemote<ControlDetailed>
    {
        public long Id { get; set; }
    }
}
