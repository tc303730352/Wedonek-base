using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePrower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleOperateId : RpcRemoteArray<long>
    {
        public long RoleId { get; set; }
        public long ProwerId { get; set; }
    }
}
