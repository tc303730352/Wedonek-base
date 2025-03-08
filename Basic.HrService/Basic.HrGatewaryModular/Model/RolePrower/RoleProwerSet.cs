using WeDonekRpc.Helper.Validate;

namespace Basic.HrGatewaryModular.Model.RolePrower
{
    public class RoleProwerSet
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NumValidate("hr.role.id.error", 1)]
        public long RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [NumValidate("hr.prower.id.error", 1)]
        public long ProwerId { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public long[] OperateId { get; set; }
    }
}
