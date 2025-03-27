using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    [IRemoteConfig("basic.hr.service")]
    public class GetTallyTrees : RpcRemote<ComTallyTree>
    {
        /// <summary>
        /// 部门查询参数
        /// </summary>
        public DeptTallyGetParam Param { get; set; }
    }
}
