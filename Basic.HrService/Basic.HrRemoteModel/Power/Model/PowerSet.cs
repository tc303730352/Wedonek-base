namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerSet
    {
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
        /// 页面参数
        /// </summary>
        public Dictionary<string, string> PageParam { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }
    }
}
