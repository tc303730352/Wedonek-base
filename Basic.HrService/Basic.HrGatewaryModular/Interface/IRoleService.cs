using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IRoleService
    {
        RoleSelectItem[] GetRoleSelect ( long companyId );
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="datum">角色资料</param>
        /// <returns></returns>
        long AddRole ( RoleSet datum, long companyId );

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        void DeleteRole ( long id );

        /// <summary>
        /// 获取角色详细
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色详细</returns>
        RoleDetailed GetRoleDetailed ( long id );


        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <param name="datum">角色资料</param>
        /// <returns></returns>
        bool SetRole ( long id, RoleSet datum );


        PagingResult<RoleDatum> Query ( PagingParam<RoleGetParam> param );
        void SetIsEnable ( long id, bool isEnable );
        void SetIsDefRole ( long id );
        void SetIsAdmin ( long id, bool isAdmin );
    }
}
