using Basic.HrGatewaryModular.Interface;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

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
        public void Set ( LongParam<long[]> param )
        {
            this._Service.Set(param.Id, param.Value);
        }
    }
}
