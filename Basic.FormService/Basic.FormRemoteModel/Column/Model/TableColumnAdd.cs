﻿using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Column.Model
{
    public class TableColumnAdd
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        [NumValidate("form.id.error", 1)]
        public long FormId { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        [NumValidate("form.table.id.error", 1)]
        public long TableId { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        [NumValidate("form.group.id.error", 0)]
        public long GroupId { get; set; }

        /// <summary>
        /// 控件ID
        /// </summary>
        [NumValidate("form.control.id.error", 1)]
        public long ControlId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [NullValidate("form.column.title.null")]
        [LenValidate("form.column.title.len", 1, 50)]
        public string ColTitle { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [NullValidate("form.column.name.null")]
        [LenValidate("form.column.name.len", 1, 50)]
        public string ColName { get; set; }

        /// <summary>
        /// 占用的列数
        /// </summary>
        [NumValidate("form.column.col.span.error", 1, 24)]
        public int ColSpan { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        [EnumValidate("form.column.type.error", typeof(ControlType))]
        public ControlType ColType { get; set; }

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
