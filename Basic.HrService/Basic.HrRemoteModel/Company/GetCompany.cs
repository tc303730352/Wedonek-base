using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 获取公司信息
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetCompany : RpcRemote<CompanyDto>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }
    }
}
