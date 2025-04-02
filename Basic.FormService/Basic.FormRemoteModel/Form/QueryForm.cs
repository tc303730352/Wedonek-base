using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormRemoteModel.Form
{
    [IRemoteConfig("basic.form.service")]
    public class QueryForm : BasicPage<FormDto>
    {
        public FormQuery Query { get; set; }
    }
}
