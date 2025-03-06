using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.LoginUser
{
    /// <summary>
    /// 删除账户
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteAccount : RpcRemote
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }
    }
}
