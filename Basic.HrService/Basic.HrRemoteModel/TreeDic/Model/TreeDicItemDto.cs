namespace Basic.HrRemoteModel.TreeDic.Model
{
    public class TreeDicItemDto
    {
        /// <summary>
        /// 树形字典项ID
        /// </summary>
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
        /// 父级字典值
        /// </summary>
        public string ParentValue { get; set; }
        /// <summary>
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }

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
        /// 字典层级
        /// </summary>
        public int DicLvl
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
    }
}
