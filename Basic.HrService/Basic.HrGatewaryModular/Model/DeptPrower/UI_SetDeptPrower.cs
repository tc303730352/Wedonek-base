using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.DeptPrower
{
    /// <summary>
    /// 设置员工部门权限 UI参数实体
    /// </summary>
    internal class UI_SetDeptPrower
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [NumValidate("hr.emp.Id.error", 1)]
        public long EmpId { get; set; }

        [NumValidate("hr.company.Id.error", 1)]
        public long CompanyId { get; set; }
        /// <summary>
        /// 拥有的部门ID
        /// </summary>
        [NullValidate("hr.dept.id.null")]
        public long[] DeptId { get; set; }

    }
}
