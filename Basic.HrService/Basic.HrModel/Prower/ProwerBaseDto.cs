using Basic.HrRemoteModel;

namespace Basic.HrModel.Prower
{
    public class ProwerBaseDto
    {
        /// <summary>
        /// 目录权限ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 名字
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
        /// 权限类型
        /// </summary>
        public ProwerType ProwerType { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int LevelNum { get; set; }

        /// <summary>
        /// 页面路由名
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
