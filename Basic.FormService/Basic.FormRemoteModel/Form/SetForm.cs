using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Form
{
    [IRemoteConfig("basic.form.service")]
    public class SetForm : RpcRemote<bool>
    {
        public long Id { get; set; }

        public FormSet Datum { get; set; }
    }
}
