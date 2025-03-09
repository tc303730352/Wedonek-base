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
        /// 入职单位
        /// </summary>
        public long UnitId { get; set; }

        /// <summary>
        /// 入职部门
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 入职公司
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string[] Title { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string[] Power { get; set; }
        /// <summary>
        /// 员工名
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

    }
}
