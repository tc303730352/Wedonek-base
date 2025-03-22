namespace Basic.HrModel.Company
{
    public class CompanyName
    {
        public long Id { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string ShortName { get; set; }
    }
}
