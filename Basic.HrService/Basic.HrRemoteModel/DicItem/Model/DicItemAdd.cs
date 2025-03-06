using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.DicItem.Model
{
    public class DicItemAdd : DicItemSet
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        [NumValidate("hr.dic.id.error", 1)]
        public long DicId
        {
            get;
            set;
        }
        /// <summary>
        /// 字典值
        /// </summary>
        [LenValidate("hr.dic.item.value.len", 0, 50)]
        public string DicValue
        {
            get;
            set;
        }
    }
}
