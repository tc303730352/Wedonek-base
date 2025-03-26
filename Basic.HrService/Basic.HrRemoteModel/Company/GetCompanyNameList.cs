using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyNameList : RpcRemoteArray<string>
    {
        public long[] Ids { get; set; }
    }
}
