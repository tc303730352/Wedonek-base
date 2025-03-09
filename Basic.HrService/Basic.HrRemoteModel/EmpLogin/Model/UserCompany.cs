namespace Basic.HrRemoteModel.EmpLogin.Model
{
    public class UserCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 任职部门
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 职位编号
        /// </summary>
        public string TitleCode { get; set; }
        /// <summary>
        /// 职位名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 部门权限
        /// </summary>
        public long[] DeptPower { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string[] Power { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
