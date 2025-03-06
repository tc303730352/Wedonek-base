namespace Basic.HrRemoteModel.EmpLogin.Model
{
    public class ProwerRoute
    {
        public long Id { get; set; }

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
        /// 布局
        /// </summary>
        public string Layout { get; set; }

        public Dictionary<string, object> PageParam { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public ProwerRoute[] Children { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public ProwerType ProwerType { get; set; }
    }
}
