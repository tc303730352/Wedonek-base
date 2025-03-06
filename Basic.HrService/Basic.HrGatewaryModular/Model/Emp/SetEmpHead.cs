using WeDonekRpc.Helper.Validate;

namespace Basic.HrGatewaryModular.Model.Emp
{
    public class SetEmpHead
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [NumValidate("hr.emp.id.error", 1)]
        public long Id { get; set; }

        [LenValidate("hr.head.url.len", 50, 255)]
        [FormatValidate("hr.head.url.error", ValidateFormat.图片URI)]
        public string HeadUrl { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        [NumValidate("hr.file.id.error", 1)]
        public long FileId { get; set; }

    }
}
