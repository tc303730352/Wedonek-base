using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("EmpTitle")]
    public class DBEmpTitle
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 员工公司ID
        /// </summary>
        public long CompanyId { get; set; }
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
