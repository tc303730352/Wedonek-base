using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    /// <summary>
    /// 获取角色详细
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleDetailed : RpcRemote<RoleDetailed>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }
    }
}
