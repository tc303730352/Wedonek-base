using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.TreeDic.Model
{
    public class TreeDicItemAdd : TreeDicItemSet
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
        /// 父ID
        /// </summary>
        [NumValidate("hr.dic.tree.parent.id.error", 0)]
        public long ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 字典值
        /// </summary>
        [LenValidate("hr.dic.tree.value.len", 0, 20)]
        public string DicValue
        {
            get;
            set;
        }
    }
}
