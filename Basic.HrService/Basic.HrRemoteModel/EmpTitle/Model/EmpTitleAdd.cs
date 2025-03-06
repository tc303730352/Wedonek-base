using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.EmpTitle.Model
{
    public class EmpTitleAdd
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [NumValidate("hr.emp.id.error", 1)]
        public long EmpId { get; set; }

        /// <summary>
        /// 员工公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long DeptId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 职位编号
        /// </summary> 
        [NullValidate("hr.title.code.null")]
        [LenValidate("hr.title.code.len", 2, 20)]
        [FormatValidate("hr.title.code.error", ValidateFormat.数字字母)]
        public string TitleCode { get; set; }

    }
}
