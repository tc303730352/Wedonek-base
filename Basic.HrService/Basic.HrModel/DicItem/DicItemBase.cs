namespace Basic.HrModel.DicItem
{
    public class DicItemBase
    {
        public long DicId { get; set; }

        /// <summary>
        /// 字典文本
        /// </summary>
        public string DicText
        {
            get;
            set;
        }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DicValue
        {
            get;
            set;
        }
    }
}
