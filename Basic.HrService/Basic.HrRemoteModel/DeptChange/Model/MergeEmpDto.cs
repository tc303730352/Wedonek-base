namespace Basic.HrRemoteModel.DeptChange.Model
{
    public class MergeEmpDto
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
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }


        /// <summary>
        /// 入职职务信息
        /// </summary>
        public string[] Title { get; set; }

        /// <summary>
        /// 职务ID
        /// </summary>
        public string[] TitleId { get; set; }

        /// <summary>
        /// 目标部门所拥有职位ID
        /// </summary>
        public string[] ToTitleId { get; set; }

        /// <summary>
        /// 目标部门所拥有职位
        /// </summary>
        public string[] ToTitle { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public HrEmpStatus Status { get; set; }
    }
}
