using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 启用部门
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EnableDept : RpcRemote<bool>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long[] DeptId { get; set; }
    }
}
