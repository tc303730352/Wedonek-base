using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginLog
{
    [IRemoteConfig("basic.hr.service")]
    public class QueryLoginLog : BasicPage<UserLoginLog>
    {
        public LoginLogQuery Query { get; set; }
    }
}
