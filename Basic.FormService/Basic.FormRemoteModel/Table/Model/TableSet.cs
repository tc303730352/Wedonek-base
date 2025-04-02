using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Table.Model
{
    public class TableSet
    {
        /// <summary>
        /// 单一表单每行列数
        /// </summary>
        public int? Columns { get; set; }

        /// <summary>
        /// 列表行自动生成方式
        /// </summary>
        [NullValidate("form.table.row.generate.mode.null")]
        [LenValidate("form.table.row.generate.mode.len",3)]
        public string RowGenerateMode { get; set; }

        /// <summary>
        /// 自动生成的列
        /// </summary>
        public string GenerateCol { get; set; }

        /// <summary>
        /// 固定行数
        /// </summary>
        public int? FixedRowNum { get; set; }

        /// <summary>
        /// 填充列和值(和固定行数配合使用)
        /// </summary>
        public Dictionary<string, string> FillColumn { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPaging { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 是否行验证
        /// </summary>
        public bool? IsRowCheck { get; set; }
    }
}
