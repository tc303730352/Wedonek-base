using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 删除公司
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteCompany : RpcRemote
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }
    }
}
