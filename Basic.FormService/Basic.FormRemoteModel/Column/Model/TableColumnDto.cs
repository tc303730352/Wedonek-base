namespace Basic.FormRemoteModel.Column.Model
{
    public class TableColumnDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 控件ID
        /// </summary>
        public long ControlId { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// 列的别名(对应填充字段名)
        /// </summary>
        public string ColAliasName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
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
        /// 列表列宽
        /// </summary>
        public int? Width { get; set; }

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
