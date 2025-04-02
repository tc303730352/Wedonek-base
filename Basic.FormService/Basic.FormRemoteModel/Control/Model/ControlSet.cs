using WeDonekRpc.Helper.Validate;
namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlSet
    {
        /// <summary>
        /// 控件名
        /// </summary>
        [NullValidate("form.control.name.null")]
        [LenValidate("form.control.name.len", 2, 50)]
        public string Name { get; set; }

        /// <summary>
        /// 控件说明
        /// </summary>
        [LenValidate("form.control.name.len", 0, 100)]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 控件最小宽度
        /// </summary>
        public int? MinWidth { get; set; }
    }
}
