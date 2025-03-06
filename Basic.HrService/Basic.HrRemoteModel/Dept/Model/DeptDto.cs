namespace Basic.HrRemoteModel.Dept.Model
{
    /// <summary>
    /// 部门资料
    /// </summary>
    public class DeptDto
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
        /// 父级部门ID
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
        /// <summary>
        /// 管理类目
        /// </summary>
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
        /// 层级
        /// </summary>
        public int Lvl
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是分支机构
        /// </summary>
        public bool IsUnit { get; set; }
        /// <summary>
        /// 是否是末级
        /// </summary>
        public bool IsEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
    }
}
