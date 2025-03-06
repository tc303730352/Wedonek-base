using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptAdd : DeptSet
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
        /// 父级ID
        /// </summary>
        [NumValidate("hr.dept.parent.id.error", 0)]
        public long ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为独立单位
        /// </summary>
        public bool IsUnit
        {
            get;
            set;
        }


    }
}
