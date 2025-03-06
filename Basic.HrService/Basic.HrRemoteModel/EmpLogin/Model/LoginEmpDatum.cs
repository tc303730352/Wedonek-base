namespace Basic.HrRemoteModel.EmpLogin.Model
{
    /// <summary>
    /// 登陆人员资料
    /// </summary>
    public class LoginEmpDatum
    {
        /// <summary>
        /// 员工名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUri { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 所在公司
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 入职公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string[] PostCode { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 职位编号
        /// </summary>
        public string TitleCode { get; set; }

    }
}
