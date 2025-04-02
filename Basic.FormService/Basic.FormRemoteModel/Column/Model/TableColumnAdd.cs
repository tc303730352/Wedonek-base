namespace Basic.FormRemoteModel.Column.Model
{
    public class TableColumnAdd : TableColumnSet
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public long TableId { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 占用的列数
        /// </summary>
        public int ColSpan { get; set; }
    }
}
