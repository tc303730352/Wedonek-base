using WeDonekRpc.Client.Interface;

namespace Basic.HrRemoteModel.Company.Model
{
    public class CompanyTreeItem : IMapperTree<CompanyTreeItem>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 全称
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
        /// 下级
        /// </summary>
        public CompanyTreeItem[] Children { get; set; }
    }
}
