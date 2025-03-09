using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("RolePower")]
    public class DBRolePower
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public long RoleId { get; set; }

        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        public long PowerId { get; set; }

        public PowerType PowerType { get; set; }
    }
}
