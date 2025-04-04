using Basic.FormRemoteModel;

namespace Basic.FormModel.Table
{
    public class TableBase
    {
        public long Id { get; set; }

        /// <summary>
        /// 表单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormTableType TableType
        {
            get;
            set;
        }

        /// <summary>
        /// 每行的列数
        /// </summary>
        public int? ColNum { get; set; }
        /// <summary>
        /// 文本宽度
        /// </summary>
        public int? LabelWidth { get; set; }
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
