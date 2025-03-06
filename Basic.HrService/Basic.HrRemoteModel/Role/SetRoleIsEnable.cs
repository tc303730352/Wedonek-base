using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Role
{
    [IRemoteConfig("basic.hr.service")]
    public class SetRoleIsEnable : RpcRemote
    {
        public long Id
        {
            get;
            set;
        }
        public bool IsEnable
        {
            get;
            set;
        }
    }
}
