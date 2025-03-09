using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.RolePower;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 角色权限接口
    /// </summary>
    internal class RolePowerApi : ApiController
    {
        private readonly IRolePowerService _Service;

        public RolePowerApi ( IRolePowerService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 修改权限集
        /// </summary>
        /// <param name="param"></param>
        public void Set ( RolePowerSet param )
        {
            this._Service.Set(param);
        }
    }
}
