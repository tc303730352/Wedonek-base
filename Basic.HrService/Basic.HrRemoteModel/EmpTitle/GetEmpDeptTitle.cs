using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpTitle
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpDeptTitle : RpcRemoteArray<string>
    {
        public long EmpId
        {
            get;
            set;
        }
        public long DeptId { get; set; }
    }
}
