using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("CompanyPowerList")]
    public class DBCompanyPowerList
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public long PowerId { get; set; }
    }
}
