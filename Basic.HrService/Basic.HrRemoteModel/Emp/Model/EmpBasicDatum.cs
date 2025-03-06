namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpBasicDatum
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
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHead { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HrUserType UserType { get; set; }

        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public EmpTitle[] Title { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 入职部门职务信息
        /// </summary>
        public string DeptTitle { get; set; }
        /// <summary>
        /// 入职部门职务ID
        /// </summary>
        public string[] DeptTitleId { get; set; }
    }
}
