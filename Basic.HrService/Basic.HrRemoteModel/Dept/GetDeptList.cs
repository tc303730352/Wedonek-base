using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDeptList : RpcRemoteArray<DeptFullTree>
    {
        public DeptQueryParam Query { get; set; }
    }
}
