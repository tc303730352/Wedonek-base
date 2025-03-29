namespace Basic.HrRemoteModel.EmpLogin.Model
{
    /// <summary>
    /// 登陆结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 当前公司
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string[] Power { get; set; }

        /// <summary>
        /// 入职的公司列表
        /// </summary>
        public long[] Company { get; set; }

        /// <summary>
        /// 部门权限
        /// </summary>
        public long[] DeptId { get; set; }

        public string EmpName { get; set; }

        public string DeptName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
