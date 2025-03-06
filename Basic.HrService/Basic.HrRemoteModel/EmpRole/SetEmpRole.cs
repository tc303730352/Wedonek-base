using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpRole
{
    /// <summary>
    /// 修改人员角色
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetEmpRole : RpcRemote
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }
        /// <summary>
        /// 人员角色列表
        /// </summary>
        public long[] RoleId { get; set; }

    }
}
