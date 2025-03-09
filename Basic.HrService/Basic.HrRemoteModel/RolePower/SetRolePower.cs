using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePower
{
    /// <summary>
    /// 添加角色操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetRolePower : RpcRemote
    {
        public long RoleId { get; set; }

        public long PowerId { get; set; }

        public long[] OperateId { get; set; }
    }
}
