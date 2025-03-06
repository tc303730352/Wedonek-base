using Basic.HrRemoteModel;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Company
{
    /// <summary>
    /// 获取公司列表树形结构 UI参数实体
    /// </summary>
    internal class UI_GetCompanyTree
    {
        /// <summary>
        /// 父级公司ID
        /// </summary>
        [NumValidate("hr.company.parent.id.error", 1)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 公司状态
        /// </summary>
        [EnumValidate("hr.company.status.null", typeof(HrCompanyStatus))]
        public HrCompanyStatus[] Status { get; set; }

    }
}
