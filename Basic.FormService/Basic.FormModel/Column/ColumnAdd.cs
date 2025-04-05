using Basic.FormRemoteModel;

namespace Basic.FormModel.Column
{
    public class ColumnAdd
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
        /// 控件ID
        /// </summary>
        public long ControlId { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string ColTitle { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ColType { get; set; }

        /// <summary>
        /// 占用的列数
        /// </summary>
        public int ColSpan { get; set; }

        /// <summary>
        /// 列表列宽
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 编辑控件
        /// </summary>
        public string EditControl { get; set; }

        /// <summary>
        /// 显示控件
        /// </summary>
        public string ShowControl { get; set; }

    }
}
