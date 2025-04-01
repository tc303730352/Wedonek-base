using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlConfig
    {
        /// <summary>
        /// 配置项名
        /// </summary>
        [NullValidate("form.control.config.key.null")]
        [LenValidate("form.control.config.key.len", 2, 50)]
        [FormatValidate("form.control.config.key.len", ValidateFormat.纯字母)]
        public string Key
        {
            get;
            set;
        }
        /// <summary>
        /// 配置项文本
        /// </summary>
        [NullValidate("form.control.config.label.null")]
        [LenValidate("form.control.config.label.len", 2, 50)]
        public string Label
        {
            get;
            set;
        }
        /// <summary>
        /// 控件值范围
        /// </summary>
        [EnumValidate("form.control.config.type.error", typeof(ControlValueRange))]
        public ControlValueRange Type { get; set; }

        /// <summary>
        /// 对应的字典项
        /// </summary>
        [NumValidate("form.control.dic.id.error", 1)]
        public long? DicId { get; set; }

        /// <summary>
        /// 文本说明
        /// </summary>
        [LenValidate("form.control.config.placeholder.len", 0, 50)]
        public string Placeholder { get; set; }

        public KeyValuePair<string, string>[] Items { get; set; }

        [LenValidate("form.control.config.def.value.len", 0, 50)]
        public string DefValue { get; set; }
    }
}
