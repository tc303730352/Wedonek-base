namespace Basic.HrModel.Dept
{
    public class DeptBase
    {
        public long Id
        {
            get;
            set;
        }
        public long CompanyId
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
        public int Lvl { get; set; }
        public bool IsUnit { get; set; }
        /// <summary>
        /// 独立单位ID
        /// </summary>
        public long UnitId { get; set; }


        public long? LeaderId
        {
            get;
            set;
        }
    }
}
