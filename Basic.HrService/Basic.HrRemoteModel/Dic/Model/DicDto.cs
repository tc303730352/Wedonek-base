namespace Basic.HrRemoteModel.Dic.Model
{
    public class DicDto
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 字典名
        /// </summary>
        public string DicName
        {
            get;
            set;
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 字典状态
        /// </summary>
        public DicStatus Status
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是系统字典
        /// </summary>
        public bool IsSysDic { get; set; }
        /// <summary>
        /// 是否为树形字典
        /// </summary>
        public bool IsTreeDic { get; set; }
    }
}
