using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.OpPrower;
using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;


namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 操作权限接口
    /// </summary>
    internal class OperateProwerApi : ApiController
    {
        private readonly IOperateProwerService _Service;

        public OperateProwerApi ( IOperateProwerService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 添加操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long Add ( OperateProwerAdd data )
        {
            return this._Service.Add(data);
        }

        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="prowerId">权限ID</param>
        /// <returns></returns>
        public OperatePrower[] GetEnables ( [NumValidate("hr.prower.id.error", 1)] long roleId, [NumValidate("hr.prower.id.error", 1)] long prowerId )
        {
            return this._Service.GetEnables(roleId, prowerId);
        }
        /// <summary>
        /// 获取操作权限集
        /// </summary>
        /// <param name="prowerId"></param>
        /// <returns></returns>
        public OperateProwerDto[] Gets ( [NumValidate("hr.prower.id.error", 1)] long prowerId )
        {
            return this._Service.Gets(prowerId);
        }
        /// <summary>
        /// 设置操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Set ( LongParam<OperateProwerSet> data )
        {
            return this._Service.Set(data.Id, data.Value);
        }

        /// <summary>
        /// 启用操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetIsEnable ( LongParam<bool> data )
        {
            return this._Service.SetIsEnable(data.Id, data.Value);
        }
    }
}
