namespace Basic.HrRemoteModel.Power.Model
{
    /// <summary>
    /// 目录权限子系统
    /// </summary>
    public class PowerSubSystem
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
        /// 目录权限树
        /// </summary>
        public PowerTree[] Powers { get; set; }

    }
}
