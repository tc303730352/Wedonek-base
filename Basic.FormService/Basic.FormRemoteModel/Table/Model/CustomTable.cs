namespace Basic.FormRemoteModel.Table.Model
{
    public class CustomTable
    {
        public long Id { get; set; }

        /// <summary>
        /// 表单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormTableType TableType
        {
            get;
            set;
        }

        /// <summary>
        /// 表单设置
        /// </summary>
        public TableSet TableSet
        {
            get;
            set;
        }
        /// <summary>
        /// 是否隐藏标题
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
