using WeDonekRpc.Helper.Validate;

namespace Basic.HrGatewaryModular.Model.RolePower
{
    public class RolePowerSet
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NumValidate("hr.role.id.error", 1)]
        public long RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [NumValidate("hr.power.id.error", 1)]
        public long PowerId { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public long[] OperateId { get; set; }
    }
}
