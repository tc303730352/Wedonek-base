using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerAdd
    {
        /// <summary>
        /// 子系统ID
        /// </summary>
        [NumValidate("hr.sub.system.id.error", 1)]
        public long SubSystemId { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [NumValidate("hr.power.parent.id.error", 0)]
        public long ParentId { get; set; }

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
        /// 权限类型
        /// </summary>
        [EnumValidate("hr.power.type.error", typeof(PowerType))]
        public PowerType PowerType { get; set; }

        /// <summary>
        /// 页面路由路径
        /// </summary>
        [EntrustValidate("_Check")]
        [LenValidate("hr.power.route.path.len", 0, 100)]
        [FormatValidate("hr.power.route.path.error", ValidateFormat.相对路径)]
        public string RoutePath { get; set; }

        /// <summary>
        /// 页面路由名
        /// </summary>
        [EntrustValidate("_Check")]
        [LenValidate("hr.power.route.name.len", 0, 50)]
        [FormatValidate("hr.power.route.name.error", ValidateFormat.数字字母)]
        public string RouteName { get; set; }

        /// <summary>
        /// 页面路径
        /// </summary>
        [EntrustValidate("_Check")]
        [LenValidate("hr.power.page.path.len", 0, 100)]
        [FormatValidate("hr.power.page.path.error", ValidateFormat.相对路径)]
        public string PagePath { get; set; }

        /// <summary>
        /// 页面参数
        /// </summary>
        public Dictionary<string, string> PageParam { get; set; }


        private void _Check ()
        {
            if ( this.PowerType == PowerType.dir )
            {
                return;
            }
            else if ( this.RoutePath.IsNull() )
            {
                throw new ErrorException("hr.power.route.path.null");
            }
            else if ( this.RouteName.IsNull() )
            {
                throw new ErrorException("hr.power.route.name.null");
            }
            else if ( this.PagePath.IsNull() )
            {
                throw new ErrorException("hr.power.page.path.null");
            }
        }
    }
}
