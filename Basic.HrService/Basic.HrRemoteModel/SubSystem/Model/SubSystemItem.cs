namespace Basic.HrRemoteModel.SubSystem.Model
{
    public class SubSystemItem
    {
        public long Id { get; set; }

        /// <summary>
        /// 子系统名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 主页配置
        /// </summary>
        public HomeSet Home { get; set; }

        /// <summary>
        /// 登陆路由名
        /// </summary>
        public string LoginRouteName { get; set; }
    }
}
