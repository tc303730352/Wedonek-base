using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    [IRemoteConfig("basic.hr.service")]
    public class SetRoleIsAdmin : RpcRemote
    {
        public long Id
        {
            get;
            set;
        }
        public bool IsAdmin
        {
            get;
            set;
        }
    }
}
