using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Helper.Validate;
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
        /// 获取权限ID
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public long[] GetProwerId ( [NumValidate("hr.role.id.error", 1)] long roleId )
        {
            return this._Service.GetProwerId(roleId);
        }

        /// <summary>
        /// 获取权限集
        /// </summary>
        /// <param name="prowerId"></param>
        /// <returns></returns>
        public OperateProwerDto[] Gets ( [NumValidate("hr.prower.id.error", 1)] long prowerId )
        {
            return this._Service.Gets(prowerId);
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
