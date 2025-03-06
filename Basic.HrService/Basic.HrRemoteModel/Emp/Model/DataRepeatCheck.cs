using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Emp.Model
{
    public class DataRepeatCheck
    {
        /// <summary>
        /// 公司
        /// </summary>
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId { get; set; }

        /// <summary>
        /// 检查类型
        /// </summary>
        [EnumValidate("hr.repeat.check.type.error", typeof(HrEmpRepeatCheckType))]
        public HrEmpRepeatCheckType CheckType { get; set; }

        /// <summary>
        /// 检查的值
        /// </summary>
        [NullValidate("hr.value.null")]
        public string Value { get; set; }

        /// <summary>
        /// 排除的人员ID
        /// </summary>
        public long? EmpId { get; set; }
    }
}
