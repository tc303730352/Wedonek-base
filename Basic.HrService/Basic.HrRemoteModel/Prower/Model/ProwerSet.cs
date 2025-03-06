namespace Basic.HrRemoteModel.Prower.Model
{
    public class ProwerSet
    {
        /// <summary>
        /// 权限码
        /// </summary>
        public string ProwerCode { get; set; }
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


        public Dictionary<string, string> PageParam { get; set; }
    }
}
