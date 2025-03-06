using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpLogin
{
    /// <summary>
    /// 人员密码登陆
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class EmpPwdLogin : RpcRemote<LoginResult>
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
    }
}
