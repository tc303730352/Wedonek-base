using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Company
{
    /// <summary>
    /// 修改公司资料 UI参数实体
    /// </summary>
    internal class UI_SetCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 公司资料
        /// </summary>
        [NullValidate("hr.company.datum.null")]
        public CompanySet Datum { get; set; }

    }
}
