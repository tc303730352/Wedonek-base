namespace Basic.HrModel.EmpTitle
{
    public class EmpTitleDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 职务编号
        /// </summary>
        public string TitleCode { get; set; }
    }
}
