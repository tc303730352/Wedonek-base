using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 获取部门树形结构
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDeptTree : RpcRemoteArray<DeptTree>
    {
        /// <summary>
        /// 部门查询参数
        /// </summary>
        public DeptGetArg Param { get; set; }

    }
}
