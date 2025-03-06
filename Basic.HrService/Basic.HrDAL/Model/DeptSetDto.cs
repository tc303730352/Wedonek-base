namespace Basic.HrDAL.Model
{
    public class DeptSetDto
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
    }
}
