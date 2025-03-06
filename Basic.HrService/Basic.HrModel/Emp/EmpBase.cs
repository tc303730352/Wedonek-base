namespace Basic.HrModel.Emp
{
    public class EmpBase
    {
        public long EmpId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 入职的公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 入职的部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmpName { get; set; }

        public string PostCode { get; set; }

    }
}
