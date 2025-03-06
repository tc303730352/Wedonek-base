using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("EmpRole")]
    public class DBEmpRole
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public long EmpId { get; set; }

        public long RoleId { get; set; }
    }
}
