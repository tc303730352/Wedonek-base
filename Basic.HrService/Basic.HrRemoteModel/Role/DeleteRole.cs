using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    /// <summary>
    /// 删除角色
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteRole : RpcRemote
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }
    }
}
