using Basic.FormRemoteModel.Column.Model;

namespace Basic.FormRemoteModel.Form.Model
{
    public class FormTableColumn
    {
        public long? ColId { get; set; }

        public string ColName { get; set; }

        public string ColTitle { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ColType { get; set; }

        /// <summary>
        /// 是否不能为空
        /// </summary>
        public bool IsNotNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultVal { get; set; }

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

        public Dictionary<string, object> ControlSet { get; set; }
        public ColumnValidateRule[] ValidateRule { get; set; }
        public List<FormTableColumn> Children { get; set; }
    }
}
