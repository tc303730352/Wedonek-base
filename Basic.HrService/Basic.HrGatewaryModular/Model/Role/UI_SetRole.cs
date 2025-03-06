using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Role
{
    /// <summary>
    /// 修改角色 UI参数实体
    /// </summary>
    internal class UI_SetRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NumValidate("hr.role.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 角色资料
        /// </summary>
        [NullValidate("hr.role.datum.null")]
        public RoleSet Datum { get; set; }

    }
}
