using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.OpMenu.Model
{
    public class OpMenuAdd
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        [NullValidate("hr.operate.menu.title.null")]
        [LenValidate("hr.operate.menu.title.len", 2, 50)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 路由路径
        /// </summary>
        [NullValidate("hr.operate.route.path.null")]
        [LenValidate("hr.operate.route.path.len", 10, 2000)]
        [FormatValidate("hr.operate.route.path.error", ValidateFormat.相对路径)]
        public string RoutePath
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        [NullValidate("hr.operate.bus.type.null")]
        [LenValidate("hr.operate.bus.type.len", 3)]
        [FormatValidate("hr.operate.bus.type.error", ValidateFormat.纯数字)]
        public string BusType
        {
            get;
            set;
        }
    }
}
