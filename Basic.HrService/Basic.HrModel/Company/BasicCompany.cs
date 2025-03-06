using Basic.HrRemoteModel;

namespace Basic.HrModel.Company
{
    public class BasicCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父公司ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public HrCompanyType CompanyType { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public long? LeaverId { get; set; }

    }
}
