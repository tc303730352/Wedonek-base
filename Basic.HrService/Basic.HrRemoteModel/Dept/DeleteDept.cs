using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 删除部门
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteDept : RpcRemote
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id { get; set; }
    }
}
