﻿namespace Basic.FormRemoteModel.Form.Model
{
    public class FormBody
    {
        /// <summary>
        /// 表单分类
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 表单说明
        /// </summary>
        public string FormShow { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        public FormStatus FormStatus { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Ver { get; set; }

        /// <summary>
        /// 表列表
        /// </summary>
        public FormTableBody[] Tables { get; set; }
    }
}
