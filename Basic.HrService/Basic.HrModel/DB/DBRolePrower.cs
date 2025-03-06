using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("RolePrower")]
    public class DBRolePrower
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public long RoleId { get; set; }

        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        public long ProwerId { get; set; }

        public ProwerType ProwerType { get; set; }
    }
}
