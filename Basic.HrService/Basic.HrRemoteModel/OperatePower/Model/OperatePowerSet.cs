using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.OperatePower.Model
{
    /// <summary>
    /// 修改操作权限
    /// </summary>
    public class OperatePowerSet
    {
        /// <summary>
        /// 操作权限名
        /// </summary>
        [NullValidate("hr.operate.name.null")]
        [LenValidate("hr.operate.name.len", 0, 20)]
        public string OperateName { get; set; }

        /// <summary>
        /// 说明文字
        /// </summary>
        [LenValidate("hr.operate.show.len", 0, 100)]
        public string Show { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
