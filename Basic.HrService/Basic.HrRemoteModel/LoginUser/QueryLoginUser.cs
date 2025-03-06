using Basic.HrRemoteModel.LoginUser.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginUser
{
    /// <summary>
    /// 查询登陆账号信息
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryLoginUser : BasicPage<LoginUserDatum>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public LoginUserQuery Query { get; set; }
    }
}
