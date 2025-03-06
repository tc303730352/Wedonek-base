namespace Basic.HrGatewaryModular.Model.EmpLogin
{
    public class LoginUser
    {
        /// <summary>
        /// 所在单位ID
        /// </summary>
        public long UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 入职部门
        /// </summary>
        public long CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId
        {
            get;
            set;
        }
        /// <summary>
        /// 员工名
        /// </summary>
        public string EmpName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUri
        {
            get;
            set;
        }
    }
}
