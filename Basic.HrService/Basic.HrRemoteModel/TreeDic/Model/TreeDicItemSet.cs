using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.TreeDic.Model
{
    /// <summary>
    /// 树形字典项
    /// </summary>
    public class TreeDicItemSet
    {
        /// <summary>
        /// 字典文本
        /// </summary>
        [NullValidate("hr.dic.tree.text.null")]
        [LenValidate("hr.dic.tree.text.len", 1, 50)]
        public string DicText
        {
            get;
            set;
        }

    }
}
