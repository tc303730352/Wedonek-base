using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePrower
{
    /// <summary>
    /// 添加角色操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetRolePrower : RpcRemote
    {
        public long RoleId { get; set; }

        public long ProwerId { get; set; }
        public long[] OperateId { get; set; }
    }
}
