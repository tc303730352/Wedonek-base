using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.DicItem.Model
{
    public class DicItemQuery
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        [NumValidate("hr.dic.id.error", 1)]
        public long DicId { get; set; }

        /// <summary>
        /// 查询关键
        /// </summary>
        [LenValidate("public.query.key.len", 0, 50)]
        public string QueryKey { get; set; }

        /// <summary>
        ///字典项状态
        /// </summary>
        [EnumValidate("hr.dic.item.status.error", typeof(DicItemStatus))]
        public DicItemStatus[] Status { get; set; }
    }
}
