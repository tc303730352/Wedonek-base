using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Unit.Model
{
    public class UnitGetArg
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
        [NumValidate("hr.parent.unit.id.error", 1)]
        public long? ParentId { get; set; }
        /// <summary>
        /// 是否只返回单位
        /// </summary>
        public bool? IsUnit { get; set; }

        /// <summary>
        /// 是否只返回部门
        /// </summary>
        public bool? IsDept { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitId { get; set; }
    }
}
