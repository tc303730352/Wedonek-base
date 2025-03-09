using SqlSugar;

namespace Basic.HrModel.DB
{
    /// <summary>
    /// 角色操作权限
    /// </summary>
    [SugarTable("RoleOperatePower")]
    public class DBRoleOperatePower
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 目录权限ID
        /// </summary>
        public long PowerId { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public long OperateId { get; set; }

        /// <summary>
        /// 操作权限值
        /// </summary>
        public string OperateVal { get; set; }
    }
}
