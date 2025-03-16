namespace Basic.HrRemoteModel.LoginUser.Model
{
    /// <summary>
    /// 登陆用户资料
    /// </summary>
    public class LoginUserDatum
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HrUserSex Sex { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHead { get; set; }

        /// <summary>
        /// 拥有的角色
        /// </summary>
        public string[] Role { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 部门权限数
        /// </summary>
        public int DeptNum { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string LoginName { get; set; }
    }
}
