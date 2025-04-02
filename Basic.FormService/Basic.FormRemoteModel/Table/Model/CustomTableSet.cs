using WeDonekRpc.Helper.Validate;

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
        /// 说明
        /// </summary>
        [LenValidate("form.table.description.len", 0, 100)]
        public string Description { get; set; }


        /// <summary>
        /// 表单设置
        /// </summary>
        [NullValidate("form.table.set.null")]
        public TableSet TableSet
        {
            get;
            set;
        }
        /// <summary>
        /// 排序位
        /// </summary>
        [NumValidate("form.table.sort.error", 0)]
        public int Sort { get; set; }
        /// <summary>
        /// 是否隐藏标题
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
