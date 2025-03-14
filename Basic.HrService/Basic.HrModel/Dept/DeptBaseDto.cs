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
        public int Lvl { get; set; }

        public bool IsUnit { get; set; }


    }
}
