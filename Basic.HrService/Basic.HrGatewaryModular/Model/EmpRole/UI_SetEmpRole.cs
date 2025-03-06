using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.EmpRole
{
    /// <summary>
    /// 修改人员角色 UI参数实体
    /// </summary>
    internal class UI_SetEmpRole
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        [NumValidate("hr.emp.id.error", 1)]
        public long EmpId { get; set; }

        /// <summary>
        /// 人员角色列表
        /// </summary>
        [NullValidate("hr.emp.role.id.null")]
        public long[] RoleId { get; set; }

    }
}
