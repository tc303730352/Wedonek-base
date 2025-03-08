using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.RolePrower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetRoleProwerId : RpcRemoteArray<long>
    {
        public long RoleId { get; set; }
    }
}
