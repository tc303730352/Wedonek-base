using Basic.HrRemoteModel.DeptChange.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptChange
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDisbandedDeptEmp : RpcRemoteArray<DisbandedDeptEmp>
    {

        public DeptDisbandedArg Arg { get; set; }
    }
}
