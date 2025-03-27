using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Role;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class RoleApi : ApiController
    {
        private readonly IRoleService _Service;
        public RoleApi ( IRoleService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="datum">角色资料</param>
        /// <returns></returns>
        [ApiPower("all", "hr.role.add")]
        public long Add ( [NullValidate("hr.role.datum.null")] RoleSet datum )
        {
            return this._Service.AddRole(datum, base.UserState.ToCompanyId());
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        [ApiPower("all", "hr.role.delete")]
        public void Delete ( [NumValidate("hr.role.id.error", 1)] long id )
        {
            this._Service.DeleteRole(id);
        }
        /// <summary>
        /// 获取角色详细
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色详细</returns>
        public RoleDetailed GetDetailed ( [NumValidate("hr.role.id.error", 1)] long id )
        {
            return this._Service.GetRoleDetailed(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RoleSelectItem[] GetSelect ()
        {
            return this._Service.GetRoleSelect(base.UserState.ToCompanyId());
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="param">角色查询参数</param>
        /// <returns>角色资料</returns>
        public PagingResult<RoleDatum> Query ( PagingParam<RoleGetParam> param )
        {
            return this._Service.Query(param);
        }
        [ApiPower("all", "hr.role.set")]
        public void SetIsEnable ( LongParam<bool> set )
        {
            this._Service.SetIsEnable(set.Id, set.Value);
        }
        [ApiPower("all", "hr.role.set")]
        public void SetIsDefRole ( [NumValidate("hr.role.id.error", 1)] long id )
        {
            this._Service.SetIsDefRole(id);
        }
        [ApiPower("all", "hr.role.set")]
        public void SetIsAdmin ( LongParam<bool> set )
        {
            this._Service.SetIsAdmin(set.Id, set.Value);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [ApiPower("all", "hr.role.set")]
        public bool Set ( [NullValidate("hr.role.param.null")] UI_SetRole param )
        {
            return this._Service.SetRole(param.Id, param.Datum);
        }

    }
}
