using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptTallyGetParam
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 是否显示子公司
        /// </summary>
        public bool IsShowChildren { get; set; }
    }
}
