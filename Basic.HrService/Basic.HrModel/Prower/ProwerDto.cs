using Basic.HrRemoteModel;

namespace Basic.HrModel.Prower
{
    public class ProwerDto
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public long Id { get; set; }

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
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

    }
}
