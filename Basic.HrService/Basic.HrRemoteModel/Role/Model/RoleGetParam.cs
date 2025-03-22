using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.Role.Model
{
    public class RoleGetParam
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.compant.id.error", 1)]
        public long CompanyId { get; set; }
        /// <summary>
        /// 查询键
        /// </summary>
        [LenValidate("public.query.key.len", 0, 50)]
        public string QueryKey { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}
