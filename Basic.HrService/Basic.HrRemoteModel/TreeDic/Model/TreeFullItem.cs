namespace Basic.HrRemoteModel.TreeDic.Model
{
    public class TreeFullItem
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
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
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool IsEnd
        {
            get;
            set;
        }

        public TreeFullItem[] Children { get; set; }
    }
}
