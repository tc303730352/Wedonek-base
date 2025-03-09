using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerSet
    {
        /// <summary>
        /// 名字
        /// </summary>
        [NullValidate("hr.power.name.null")]
        [LenValidate("hr.power.name.len", 2, 50)]
        [FormatValidate("hr.power.name.error", ValidateFormat.中文字母数字括号)]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [LenValidate("hr.power.description.len", 0, 255)]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [LenValidate("hr.power.icon.len", 0, 100)]
        [FormatValidate("hr.power.icon.error", ValidateFormat.图标样式)]
        public string Icon { get; set; }

        /// <summary>
        /// 页面路由路径
        /// </summary>
        [NullValidate("hr.power.route.path.null")]
        [LenValidate("hr.power.route.path.len", 0, 100)]
        [FormatValidate("hr.power.route.path.error", ValidateFormat.相对路径)]
        public string RoutePath { get; set; }

        /// <summary>
        /// 页面路由名
        /// </summary>
        [NullValidate("hr.power.route.name.null")]
        [LenValidate("hr.power.route.name.len", 0, 50)]
        [FormatValidate("hr.power.route.name.error", ValidateFormat.数字字母)]
        public string RouteName { get; set; }

        /// <summary>
        /// 页面路径
        /// </summary>
        [NullValidate("hr.power.page.path.null")]
        [LenValidate("hr.power.page.path.len", 0, 100)]
        [FormatValidate("hr.power.page.path.error", ValidateFormat.相对路径)]
        public string PagePath { get; set; }

        /// <summary>
        /// 页面参数
        /// </summary>
        public Dictionary<string, string> PageParam { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        [NumValidate("hr.power.sort.error", 1)]
        public int Sort { get; set; }
    }
}
