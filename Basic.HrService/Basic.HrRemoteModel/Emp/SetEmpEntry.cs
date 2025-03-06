using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class SetEmpEntry : RpcRemote
    {
        public long Id { get; set; }
        public EmpEntry Datum { get; set; }
    }
}
