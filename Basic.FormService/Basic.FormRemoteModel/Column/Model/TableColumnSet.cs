using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Column.Model
{
    public class TableColumnSet
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        [NumValidate("form.control.id.error", 1)]
        public long ControlId { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [NullValidate("form.column.name.null")]
        public string ColName { get; set; }

        /// <summary>
        /// 列的别名(对应填充字段名)
        /// </summary>
        public string ColAliasName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [NullValidate("form.column.title.null")]
        [LenValidate("form.column.title.len", 1, 50)]
        public string ColTitle { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        [EnumValidate("form.column.type.error", typeof(ControlType))]
        public ControlType ColType { get; set; }

        /// <summary>
        /// 最大文本长度
        /// </summary>
        public int? MaxLen { get; set; }

        /// <summary>
        /// 是否不能为空
        /// </summary>
        public bool IsNotNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultVal { get; set; }

        /// <summary>
        /// 列表列宽
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 编辑控件
        /// </summary>
        public string EditControl { get; set; }

        /// <summary>
        /// 显示控件
        /// </summary>
        public string ShowControl { get; set; }
        /// <summary>
        /// 控件配置
        /// </summary>
        public Dictionary<string, object> ControlSet { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        public ColumnValidateRule[] ValidateRule { get; set; }
    }
}
