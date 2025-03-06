using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 修改公司资料
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetCompany : RpcRemote<bool>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 公司资料
        /// </summary>
        public CompanySet Datum { get; set; }
    }
}
