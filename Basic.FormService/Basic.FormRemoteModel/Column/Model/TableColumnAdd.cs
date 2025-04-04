using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Column.Model
{
    public class TableColumnAdd : TableColumnSet
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
        /// 占用的列数
        /// </summary>
        [NumValidate("form.table.id.error", 1, 24)]
        public int ColSpan { get; set; }
    }
}
