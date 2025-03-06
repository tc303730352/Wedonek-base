namespace Basic.HrRemoteModel.DeptChange.Model
{
    public class ChangeDeptTree
    {
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

        public bool IsUnit { get; set; }


        public ChangeDeptTree[] Children { get; set; }

        public int EmpNum { get; set; }
    }
}
