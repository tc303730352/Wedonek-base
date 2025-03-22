using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpLogin
{
    [IRemoteConfig("basic.hr.service")]
    public class EmpSwitchCompany : RpcRemote<ComSwitchResult>
    {
        public long EmpId { get; set; }

        public long CompanyId { get; set; }
    }
}
