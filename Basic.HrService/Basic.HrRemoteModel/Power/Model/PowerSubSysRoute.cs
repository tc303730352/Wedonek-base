namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerSubSysRoute
    {
        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSysId { get; set; }

        /// <summary>
        /// 子系统名
        /// </summary>
        public string SubSysName { get; set; }
        /// <summary>
        /// 主页地址
        /// </summary>
        public string HomePath { get; set; }

        /// <summary>
        /// 下级路由
        /// </summary>
        public PowerRoute[] Routes { get; set; }
    }
}
