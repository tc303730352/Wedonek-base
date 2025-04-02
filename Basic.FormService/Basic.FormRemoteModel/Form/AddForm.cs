using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Form
{
    [IRemoteConfig("basic.form.service")]
    public class AddForm : RpcRemote<long>
    {
        public FormAdd Datum { get; set; }
    }
}
