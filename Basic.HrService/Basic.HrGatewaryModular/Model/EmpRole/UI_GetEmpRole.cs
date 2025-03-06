using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.EmpRole
{
    /// <summary>
    /// 获取人员角色列表 UI参数实体
    /// </summary>
    internal class UI_GetEmpRole
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        [NumValidate("hr.emp.id.error", 1)]
        public long EmpId { get; set; }

        /// <summary>
        /// 公司id
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

    }
}
