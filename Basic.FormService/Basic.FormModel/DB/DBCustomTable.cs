using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Table.Model;
using SqlSugar;

namespace Basic.FormModel.DB
{
    [SugarTable("CustomTable")]
    public class DBCustomTable
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 所属表单ID
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormTableType TableType
        {
            get;
            set;
        }

        /// <summary>
        /// 表单设置
        /// </summary>
        [SugarColumn(IsJson = true)]
        public TableSet TableSet
        {
            get;
            set;
        }
        /// <summary>
        /// 每行的列数
        /// </summary>
        public int? Columns { get; set; }
        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否隐藏标题
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
