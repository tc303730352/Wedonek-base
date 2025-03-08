using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.RolePrower;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 角色权限接口
    /// </summary>
    internal class RoleProwerApi : ApiController
    {
        private readonly IRoleProwerService _Service;

        public RoleProwerApi ( IRoleProwerService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 修改权限集
        /// </summary>
        /// <param name="param"></param>
        public void Set ( RoleProwerSet param )
        {
            this._Service.Set(param);
        }
    }
}
