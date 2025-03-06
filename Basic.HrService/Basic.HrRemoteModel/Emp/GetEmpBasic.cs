using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpBasic : RpcRemote<EmpBasicDatum>
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
    }
}
