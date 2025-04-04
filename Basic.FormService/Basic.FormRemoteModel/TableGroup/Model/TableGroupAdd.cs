namespace Basic.FormRemoteModel.TableGroup.Model
{
    public class TableGroupAdd : TableGroupSet
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public long TableId { get; set; }

        /// <summary>
        /// 父集ID
        /// </summary>
        public long ParentId { get; set; }
    }
}
