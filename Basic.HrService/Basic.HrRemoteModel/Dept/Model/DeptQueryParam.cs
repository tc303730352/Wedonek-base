using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptQueryParam
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
        /// 部门名
        /// </summary>
        public string QueryKey
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是分公司
        /// </summary>
        public bool? IsUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 是否包括所有下级
        /// </summary>
        public bool IsAllChildren
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus[] Status
        {
            get;
            set;
        }
    }
}
