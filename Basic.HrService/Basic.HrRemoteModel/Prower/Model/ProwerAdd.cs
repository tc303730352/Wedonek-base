namespace Basic.HrRemoteModel.Prower.Model
{
    public class ProwerAdd
    {
        /// <summary>
        /// 权限码
        /// </summary>
        public string ProwerCode { get; set; }
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

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
        /// 页面路由路径
        /// </summary>
        public string RoutePath { get; set; }

        /// <summary>
        /// 页面路由名
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PagePath { get; set; }

        /// <summary>
        /// 页面参数
        /// </summary>
        public Dictionary<string, string> PageParam { get; set; }

    }
}
