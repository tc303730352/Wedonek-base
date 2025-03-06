namespace Basic.HrModel.SubSystem
{
    public class SubSystemDto
    {
        /// <summary>
        /// 子系统ID
        /// </summary>
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
        /// 登陆路由名
        /// </summary>
        public string LoginRouteName { get; set; }
    }
}
