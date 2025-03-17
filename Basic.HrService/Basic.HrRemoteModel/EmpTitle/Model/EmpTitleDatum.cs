namespace Basic.HrRemoteModel.EmpTitle.Model
{
    public class EmpTitleDatum
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 单位名
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 部门名
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
    }
}
