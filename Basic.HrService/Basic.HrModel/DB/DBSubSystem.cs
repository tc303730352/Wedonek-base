using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("SubSystem")]
    public class DBSubSystem
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 子系统名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 登陆路由名
        /// </summary>
        public string LoginRouteName { get; set; }
    }
}
