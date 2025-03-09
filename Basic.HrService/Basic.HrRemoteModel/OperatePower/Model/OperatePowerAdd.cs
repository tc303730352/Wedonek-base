using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.OperatePower.Model
{
    /// <summary>
    /// 添加操作权限
    /// </summary>
    public class OperatePowerAdd : OperatePowerSet
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        [NumValidate("hr.power.id.error", 1)]
        public long PowerId { get; set; }

        /// <summary>
        /// 操作权限值
        /// </summary>
        [NullValidate("hr.operate.value.null")]
        [LenValidate("hr.operate.value.len", 0, 50)]
        [FormatValidate("hr.operate.value.error", ValidateFormat.字母点)]
        public string OperateVal { get; set; }
    }
}
