namespace Basic.HrRemoteModel.DicItem.Model
{
    public class DicItemDto
    {
        public long Id { get; set; }


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
        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 字典状态
        /// </summary>
        public DicItemStatus DicStatus
        {
            get;
            set;
        }
    }
}
