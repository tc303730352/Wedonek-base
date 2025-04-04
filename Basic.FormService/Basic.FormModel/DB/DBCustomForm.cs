﻿using Basic.FormRemoteModel;
using SqlSugar;

namespace Basic.FormModel.DB
{
    [SugarTable("CustomForm")]
    public class DBCustomForm
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id
        {
            get;
            set;
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

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
        /// 是否删除
        /// </summary>
        public bool IsDrop { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
