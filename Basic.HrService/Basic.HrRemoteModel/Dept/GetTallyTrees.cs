using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    [IRemoteConfig("basic.hr.service")]
    public class GetTallyTrees : RpcRemoteArray<DeptTallyTree>
    {
        /// <summary>
        /// 部门查询参数
        /// </summary>
        public DeptGetArg Param { get; set; }
    }
}
