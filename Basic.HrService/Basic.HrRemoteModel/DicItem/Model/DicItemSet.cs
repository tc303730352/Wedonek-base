using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.DicItem.Model
{
    public class DicItemSet
    {

        /// <summary>
        /// 字典文本
        /// </summary>
        [NullValidate("hr.dic.item.text.null")]
        [LenValidate("hr.dic.item.text.len", 1, 50)]
        public string DicText
        {
            get;
            set;
        }

    }
}
