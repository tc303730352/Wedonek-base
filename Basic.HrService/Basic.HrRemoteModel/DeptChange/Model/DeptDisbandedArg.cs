using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.DeptChange.Model
{
    public class DeptDisbandedArg
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 变动的部门ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long DeptId
        {
            get;
            set;
        }
    }
}
