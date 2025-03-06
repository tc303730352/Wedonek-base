using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpLogin
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpLoginDatum : RpcRemote<EmpLoginDatum>
    {
        public long EmpId
        {
            get;
            set;
        }
    }
}
