using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    /// <summary>
    /// 添加角色
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddRole : RpcRemote<long>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 角色资料
        /// </summary>
        public RoleSet Datum { get; set; }
    }
}
