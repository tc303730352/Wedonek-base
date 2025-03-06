using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginUser
{
    [IRemoteConfig("basic.hr.service")]
    public class UpdateUserPwd : RpcRemote
    {
        public long EmpId { get; set; }

        public PwdSetArg SetArg { get; set; }
    }
}
