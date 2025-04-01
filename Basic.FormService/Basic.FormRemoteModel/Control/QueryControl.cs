using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Control
{
    [IRemoteConfig("basic.form.service")]
    public class QueryControl : BasicPage<ControlDto>
    {
        public ControlQuery Query { get; set; }
    }
}
