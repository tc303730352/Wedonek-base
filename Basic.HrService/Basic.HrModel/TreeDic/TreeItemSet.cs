using Basic.HrRemoteModel;

namespace Basic.HrModel.TreeDic
{
    public class TreeItemSet
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
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 字典层级
        /// </summary>
        public int DicLvl
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
        /// 排序位
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
    }
}
