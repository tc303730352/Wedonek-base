using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 获取部门选项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDeptSelect : RpcRemoteArray<DeptSelect>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public DeptGetArg GetParam { get; set; }
    }
}
