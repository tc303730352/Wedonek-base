namespace Basic.HrModel.Dept
{
    public class DeptBaseDto
    {
        public long Id
        {
            get;
            set;
        }

        public long ParentId
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
        public int Lvl { get; set; }

        public bool IsUnit { get; set; }

        public long? LeaderId
        {
            get;
            set;
        }

    }
}
