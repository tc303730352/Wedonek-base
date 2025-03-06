using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    /// <summary>
    /// 获取角色列表
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryRole : BasicPage<RoleDatum>
    {
        /// <summary>
        /// 角色查询参数
        /// </summary>
        public RoleGetParam Param { get; set; }
    }
}
