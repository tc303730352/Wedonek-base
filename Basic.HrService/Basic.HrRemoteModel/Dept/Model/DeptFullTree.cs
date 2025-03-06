namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptFullTree
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// 部门名
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门短名
        /// </summary>
        public string ShortName
        {
            get;
            set;
        }
        public string[] DeptTag
        {
            get;
            set;
        }
        /// <summary>
        /// 部门说明
        /// </summary>
        public string DeptShow
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public long? LeaderId
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人名
        /// </summary>
        public string LeaderName
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus Status { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为单位
        /// </summary>
        public bool IsUnit { get; set; }
        /// <summary>
        /// 下级部门
        /// </summary>
        public DeptFullTree[] Children { get; set; }
    }
}
