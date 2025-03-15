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
        ///   是否入职  IsEntry=true 返回在部门中拥有职务的人员 IsEntry=false 返回部门中的人员
        /// </summary>
        public bool IsEntry { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }
    }
}
