namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpTitle
    {
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public long DeptId { get; set; }

        public string TitleCode { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Show { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
    }
}
