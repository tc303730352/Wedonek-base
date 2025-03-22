namespace Basic.HrGatewaryModular.Model.EmpLogin
{
    public class LoginUser
    {
        /// <summary>
        /// 当前公司ID
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
        /// 头像
        /// </summary>
        public string HeadUri
        {
            get;
            set;
        }
    }
}
