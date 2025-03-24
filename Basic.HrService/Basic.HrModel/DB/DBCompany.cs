using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    /// <summary>
    /// 公司信息
    /// </summary>
    [SugarTable("Company")]
    public class DBCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 父公司ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }
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
        /// 负责人
        /// </summary>
        public long? LeaverId { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public long? AdminId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
