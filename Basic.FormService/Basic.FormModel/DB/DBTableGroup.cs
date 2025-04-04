using SqlSugar;

namespace Basic.FormModel.DB
{
    [SugarTable("TableGroup")]
    public class DBTableGroup
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public long TableId { get; set; }

        public long ParentId { get; set; }

        /// <summary>
        /// 表单组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否固定生成
        /// </summary>
        public bool IsFixed { get; set; }

    }
}
