using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    /// <summary>
    /// 修改角色
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetRole : RpcRemote<bool>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 角色资料
        /// </summary>
        public RoleSet Datum { get; set; }
    }
}
