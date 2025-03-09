using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("EmpDeptPower")]
    public class DBEmpDeptPower
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long EmpId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
    }
}
