using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.DeptPrower
{
    /// <summary>
    /// 获取员工部门数据权限 UI参数实体
    /// </summary>
    internal class UI_GetDeptPrower
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [NumValidate("hr.emp.Id.error", 1)]
        public long EmpId { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.Id.error", 1)]
        public long CompanyId { get; set; }

    }
}
