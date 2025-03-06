using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 停用部门
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class StopDept : RpcRemote<bool>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
    }
}
