using WeDonekRpc.Client.Interface;

namespace Basic.HrRemoteModel.Company.Model
{
    public class CompanyTree : IMapperTree<CompanyTree>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public HrCompanyType CompanyType { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public long? LeaverId { get; set; }
        /// <summary>
        /// 下级公司
        /// </summary>
        public CompanyTree[] Children { get; set; }
    }
}
