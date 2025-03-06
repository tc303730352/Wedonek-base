using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.TreeDic.Model
{
    public class TreeItemQuery
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        [NumValidate("hr.dic.id.error", 1)]
        public long DicId { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        [LenValidate("hr.query.key.len", 2, 50)]
        public string QueryKey { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DicItemStatus[] Status { get; set; }
    }
}
