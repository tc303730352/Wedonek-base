using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginUser
{
    /// <summary>
    /// 开通账号
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class OpenAccount : RpcRemote
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long[] EmpId { get; set; }
        public long CompanyId { get; set; }
    }
}
