using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 获取公司列表树形结构
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyTree : RpcRemoteArray<CompanyTree>
    {
        /// <summary>
        /// 父级公司ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 公司状态
        /// </summary>
        public HrCompanyStatus[] Status { get; set; }
    }
}
