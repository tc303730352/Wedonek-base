namespace Basic.HrRemoteModel.EmpTitle.Model
{
    public class EmpTitleData
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 职位编号
        /// </summary>
        public string TitleCode { get; set; }
        /// <summary>
        /// 职位名
        /// </summary>
        public string Title { get; set; }
    }
}
