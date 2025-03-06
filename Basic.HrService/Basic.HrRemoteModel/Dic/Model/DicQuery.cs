using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dic.Model
{
    public class DicQuery
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [LenValidate("hr.query.key.len", 0, 50)]
        public string QueryKey { get; set; }
        /// <summary>
        /// 字典状态
        /// </summary>
        [EnumValidate("hr.dic.status.error", typeof(DicStatus))]
        public DicStatus[] Status { get; set; }
        /// <summary>
        /// 是否是系统字典
        /// </summary>
        public bool? IsSysDic { get; set; }

        /// <summary>
        /// 是否是树形字典
        /// </summary>
        public bool? IsTreeDic { get; set; }
    }
}
