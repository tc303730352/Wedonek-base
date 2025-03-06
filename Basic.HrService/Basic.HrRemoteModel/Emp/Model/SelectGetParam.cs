using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Emp.Model
{
    public class SelectGetParam
    {
        /// <summary>
        /// 所在公司ID
        /// </summary>
        [NumValidate("hr.company.Id.error", 1)]
        public long CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [NumValidate("hr.dept.Id.error", 1)]
        public long DeptId { get; set; }

        /// <summary>
        /// 是否只返回入职的员工
        /// </summary>
        public bool IsEntry { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }
    }
}
