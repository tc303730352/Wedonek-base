﻿using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Table.Model
{
    public class CustomTableSet
    {
        /// <summary>
        /// 表单标题
        /// </summary>
        [NullValidate("form.table.title.null")]
        [LenValidate("form.table.title.len", 2, 50)]
        public string Title { get; set; }

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

        /// <summary>
        /// 每行的列数
        /// </summary>
        [NumValidate("form.table.label.width.error", 1, 6)]
        public int? ColNum { get; set; }
        /// <summary>
        /// 文本宽度
        /// </summary>
        [NumValidate("form.table.label.width.error", 80, 200)]
        public int? LabelWidth { get; set; }


    }
}
