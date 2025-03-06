using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("RoleList")]
    public class DBRole
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string RoleName { get; set; }

        public string Remark { get; set; }

        public bool IsDefRole { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        public bool IsEnable { get; set; }

        public DateTime AddTime { get; set; }


    }
}
