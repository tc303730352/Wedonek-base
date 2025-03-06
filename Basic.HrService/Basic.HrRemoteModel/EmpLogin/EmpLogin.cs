using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpLogin
{
    /// <summary>
    /// 人员登陆
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EmpLogin : RpcRemote<LoginResult>
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }
    }
}
