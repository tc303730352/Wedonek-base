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
        /// 部门标签(部门类型)
        /// </summary>
        public string[] DeptTag
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
        /// 部门说明
        /// </summary>
        public string DeptShow
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus Status { get; set; }
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

        /// <summary>
        /// 员工总量
        /// </summary>
        public int EmpTotal { get; set; }
    }
}
