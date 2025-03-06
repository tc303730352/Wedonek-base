using Basic.HrRemoteModel;

namespace Basic.HrModel.Dept
{
    public class DeptGetParam
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus[] Status { get; set; }
        /// <summary>
        /// 是否是单位
        /// </summary>
        public bool? IsUnit { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitId { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否所有下级
        /// </summary>
        public bool IsAllChildren { get; set; }
    }
}
