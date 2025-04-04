namespace Basic.FormModel.Group
{
    public class TableGroupBase
    {
        public long Id { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public long TableId { get; set; }

        public long ParentId { get; set; }

        /// <summary>
        /// 表单组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否隐藏标题
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
