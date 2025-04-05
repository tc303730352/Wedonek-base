using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Column.Model;
using SqlSugar;

namespace Basic.FormModel.Column
{
    public class TableColumnBase
    {
        public long Id { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public long TableId { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public long GroupId { get; set; }

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

        /// <summary>
        /// 控件配置
        /// </summary>
        [SugarColumn(IsJson = true)]
        public Dictionary<string, object> ControlSet { get; set; }
        /// <summary>
        /// 验证规则
        /// </summary>
        [SugarColumn(IsJson = true)]
        public ColumnValidateRule[] ValidateRule { get; set; }

    }
}
