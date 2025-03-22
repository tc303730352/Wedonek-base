using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.CompanyPower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyPowerId : RpcRemoteArray<long>
    {
        public long CompanyId { get; set; }
    }
}
