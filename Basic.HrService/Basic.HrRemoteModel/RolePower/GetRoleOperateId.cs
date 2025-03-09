using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleOperateId : RpcRemoteArray<long>
    {
        public long RoleId { get; set; }
        public long PowerId { get; set; }
    }
}
