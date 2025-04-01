using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlAdd : ControlSet
    {
        /// <summary>
        /// 控件类型
        /// </summary>
        [EnumValidate("form.control.type.error", typeof(ControlType))]
        public ControlType ControlType { get; set; }

        /// <summary>
        /// 编辑控件
        /// </summary>
        [LenValidate("form.control.edit.control.len", 0, 255)]
        public string EditControl { get; set; }

        /// <summary>
        /// 显示控件
        /// </summary>
        [LenValidate("form.control.name.len", 0, 255)]
        public string ShowControl { get; set; }

        /// <summary>
        /// 控件配置
        /// </summary>
        public ControlConfig[] Config { get; set; }
    }
}
