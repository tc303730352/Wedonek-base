using Basic.HrModel.DB;

namespace Basic.HrModel.Dept
{
    public class EmpDept
    {
        public long EmpId
        {
            get;
            set;
        }
        public long DeptId
        {
            get;
            set;
        }
        public long[] DeptPrower { get; set; }
        public bool IsOpenAccount { get; set; }
    }
    public class MergeDept
    {
        /// <summary>
        /// 作废的部门ID
        /// </summary>
        public DBDept ToVoid { get; set; }

        /// <summary>
        /// 目的地ID
        /// </summary>
        public DBDept ToDept { get; set; }
        /// <summary>
        /// 需要更新的人员ID
        /// </summary>
        public EmpDept[] Emp { get; set; }
        /// <summary>
        /// 添加的人员职务
        /// </summary>
        public EmpTitleAdd[] EmpTitle { get; set; }
        /// <summary>
        /// 删除的人员职务ID
        /// </summary>
        public long[] EmpTitleId { get; set; }

        public long[] DropProwerId { get; set; }
    }

    public class EmpTitleAdd
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 职务编号
        /// </summary>
        public string TitleCode { get; set; }
    }
    public class MergeSubDeptSet : SubDeptSet
    {
        public long ParentId
        {
            get;
            set;
        }
    }
}
