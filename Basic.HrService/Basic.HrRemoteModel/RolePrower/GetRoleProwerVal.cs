using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePrower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleProwerVal : RpcRemoteArray<string>
    {
        public long RoleId { get; set; }
    }
}
