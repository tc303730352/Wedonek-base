namespace Basic.HrRemoteModel.TreeDic.Model
{
    /// <summary>
    /// 树形字典
    /// </summary>
    public class TreeItemBase
    {
        public long Id
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
        /// <summary>
        /// 树形字典项下级
        /// </summary>
        public TreeItemBase[] Children { get; set; }

    }
}
