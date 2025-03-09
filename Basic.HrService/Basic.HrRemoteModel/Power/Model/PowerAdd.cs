using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Power.Model
{
    public class PowerAdd : PowerSet
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
        /// 权限类型
        /// </summary>
        [EnumValidate("hr.power.type.error", typeof(PowerType))]
        public PowerType PowerType { get; set; }

    }
}
