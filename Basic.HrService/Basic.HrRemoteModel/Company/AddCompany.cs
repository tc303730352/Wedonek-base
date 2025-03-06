using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 新增公司
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddCompany : RpcRemote<long>
    {
        /// <summary>
        /// 公司资料
        /// </summary>
        public CompanyAdd Datum { get; set; }
    }
}
