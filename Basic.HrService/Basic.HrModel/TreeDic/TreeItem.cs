namespace Basic.HrModel.TreeDic
{
    public class TreeItem
    {
        public long Id { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long ParentId
        {
            get;
            set;
        }

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
