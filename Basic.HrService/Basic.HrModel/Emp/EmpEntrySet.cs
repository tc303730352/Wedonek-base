namespace Basic.HrModel.Emp
{
    public class EmpEntrySet
    {
        /// <summary>
        /// 入职的公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 入职的部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 职位码
        /// </summary>
        public string[] Title { get; set; }

        /// <summary>
        /// 需要删除的职务ID
        /// </summary>
        public long[] DropTitleId { get; set; }

        public long UnitId { get; set; }

        /// <summary>
        /// 是否保留原职务
        /// </summary>
        public bool IsRetainTitle { get; set; }
    }
}
