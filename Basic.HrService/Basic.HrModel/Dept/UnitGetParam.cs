using Basic.HrRemoteModel;

namespace Basic.HrModel.Dept
{
    public class UnitGetParam
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus[] Status { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        public bool IsAllChildren { get; set; }
        /// <summary>
        /// 是否返回单位
        /// </summary>
        public bool? IsUnit { get; set; }
        public bool? IsDept { get; set; }
        public long? UnitId { get; set; }
    }
}
