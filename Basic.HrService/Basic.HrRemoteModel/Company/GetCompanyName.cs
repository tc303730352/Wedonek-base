using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyName : RpcRemote<string>
    {
        public long Id { get; set; }
    }
}
