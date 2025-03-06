using Basic.HrRemoteModel.DeptChange.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptChange
{
    [IRemoteConfig("basic.hr.service")]
    public class GetMergeDeptEmp : RpcRemote<MergeEmp>
    {
        public DeptMergeArg Arg { get; set; }
    }
}
