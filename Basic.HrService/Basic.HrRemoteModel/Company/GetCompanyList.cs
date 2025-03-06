using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 获取公司列表
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyList : RpcRemoteArray<CompanyDto>
    {
        /// <summary>
        /// 父级公司ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否包含所有下级
        /// </summary>
        public bool IsAllChildren { get; set; }
    }
}
