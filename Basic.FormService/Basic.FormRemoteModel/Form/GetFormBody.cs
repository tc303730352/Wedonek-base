using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Form
{
    [IRemoteConfig("basic.form.service")]
    public class GetFormBody : RpcRemote<Model.FormBody>
    {
        public long Id { get; set; }
    }
}
