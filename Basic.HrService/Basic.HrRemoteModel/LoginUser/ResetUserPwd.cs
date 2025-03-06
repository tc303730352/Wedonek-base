using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginUser
{
    [IRemoteConfig("basic.hr.service")]
    public class ResetUserPwd : RpcRemote
    {
        public long EmpId
        {
            get;
            set;
        }
    }
}
