using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Form.Model
{
    public class FormAdd : FormSet
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [NullValidate("form.ver.code.null")]
        [LenValidate("form.ver.code.len", 2, 10)]
        public string Ver {  get; set; }
    }
}
