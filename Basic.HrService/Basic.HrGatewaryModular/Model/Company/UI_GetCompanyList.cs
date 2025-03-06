using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Company
{
    /// <summary>
    /// 获取公司列表 UI参数实体
    /// </summary>
    internal class UI_GetCompanyList
    {
        /// <summary>
        /// 父级公司ID
        /// </summary>
        [NumValidate("hr.company.parent.id.error", 1)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否包含所有下级
        /// </summary>
        public bool IsAllChildren { get; set; }

    }
}
