using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Form.Model
{
    public class FormSet
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        [NullValidate("form.name.null")]
        [LenValidate("form.name.len", 2, 50)]
        public string FormName { get; set; }

        /// <summary>
        /// 表单说明
        /// </summary>
        [LenValidate("form.show.len", 0, 100)]
        public string FormShow { get; set; }

        /// <summary>
        /// 表单分类
        /// </summary>
        [NullValidate("form.type.null")]
        [LenValidate("form.type.len", 2, 6)]
        public string FormType { get; set; }
    }
}
