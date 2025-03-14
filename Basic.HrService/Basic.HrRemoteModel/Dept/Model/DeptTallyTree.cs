namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptTallyTree
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为单位
        /// </summary>
        public bool IsUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 下级
        /// </summary>
        public DeptTallyTree[] Children
        {
            get;
            set;
        }
        /// <summary>
        /// 人员数量
        /// </summary>
        public int EmpNum { get; set; }
    }
}
