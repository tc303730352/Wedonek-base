using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class AddControl : RpcRemote<long>
    {
        public ControlAdd Datum
        {
            get;
            set;
        }
    }
}
