using WeDonekRpc.Client.Interface;

namespace Basic.HrRemoteModel.Company.Model
{
    public class CompanyTree : IMapperTree<CompanyTree>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 公司状态
        /// </summary>
        public HrCompanyStatus Status { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public HrCompanyType CompanyType { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public long? LeaverId { get; set; }

        /// <summary>
        /// 管理员名
        /// </summary>
        public string Leaver { get; set; }

        /// <summary>
        /// 管理员名
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 下级公司
        /// </summary>
        public CompanyTree[] Children { get; set; }
    }
}
