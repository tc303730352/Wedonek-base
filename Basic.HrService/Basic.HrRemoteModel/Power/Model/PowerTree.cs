namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerTree
    {
        /// <summary>
        /// 权限树
        /// </summary>
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
        /// 权限类型
        /// </summary>
        public PowerType PowerType { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public PowerTree[] Children { get; set; }

    }
}
