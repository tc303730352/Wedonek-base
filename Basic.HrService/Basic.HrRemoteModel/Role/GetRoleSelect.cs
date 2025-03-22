using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleSelect : RpcRemoteArray<RoleSelectItem>
    {
        public long CompanyId { get; set; }
    }
}
