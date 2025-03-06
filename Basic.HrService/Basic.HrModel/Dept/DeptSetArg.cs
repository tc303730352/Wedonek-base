namespace Basic.HrModel.Dept
{
    public class DeptSetArg
    {
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
        /// 父级部门ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 部门标签
        /// </summary>
        public string DeptTag { get; set; }

        /// <summary>
        /// 部门说明
        /// </summary>
        public string DeptShow
        {
            get;
            set;
        }
        /// <summary>
        /// 部门负责人
        /// </summary>
        public long? LeaderId
        {
            get;
            set;
        }
        public string LevelCode
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
        /// 层级
        /// </summary>
        public int Lvl
        {
            get;
            set;
        }
    }
    public class SubDeptSet
    {
        public long Id
        {
            get;
            set;
        }
        public string LevelCode
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
    }
}
