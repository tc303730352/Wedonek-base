using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dept.Model
{
    /// <summary>
    /// 部门查询参数
    /// </summary>
    public class DeptGetArg
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 父级部门
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus[] Status { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long[] DeptId { get; set; }

    }
}
