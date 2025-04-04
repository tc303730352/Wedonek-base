using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Table.Model
{
    public class CustomTableAdd : CustomTableSet
    {
        /// <summary>
        /// 所属表单ID
        /// </summary>
        [NumValidate("form.id.error", 1)]
        public long FormId { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        [EnumValidate("form.table.type.error", typeof(FormTableType))]
        public FormTableType TableType
        {
            get;
            set;
        }

    }
}
