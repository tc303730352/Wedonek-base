using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    [IRemoteConfig("basic.hr.service")]
    public class SetIsDefRole : RpcRemote
    {
        public long Id { get; set; }
    }
}
