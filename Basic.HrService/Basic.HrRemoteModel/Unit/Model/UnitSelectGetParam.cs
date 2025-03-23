using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Unit.Model
{
    public class UnitSelectGetParam
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus[] Status { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long? ParentId { get; set; }

        public long[] DeptId { get; set; }
    }
}
