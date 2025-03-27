using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.OpPower;
using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;


namespace Basic.HrGatewaryModular.Api
{
    /// <summary>
    /// 操作权限接口
    /// </summary>
    internal class OperatePowerApi : ApiController
    {
        private readonly IOperatePowerService _Service;

        public OperatePowerApi ( IOperatePowerService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 添加操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiPower("all", "hr.power.add")]
        public long Add ( OperatePowerAdd data )
        {
            return this._Service.Add(data);
        }

        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="powerId">权限ID</param>
        /// <returns></returns>
        public OperatePower GetEnables ( [NumValidate("hr.role.id.error", 1)] long roleId, [NumValidate("hr.power.id.error", 1)] long powerId )
        {
            return this._Service.GetEnables(roleId, powerId);
        }
        /// <summary>
        /// 获取操作权限集
        /// </summary>
        /// <param name="powerId"></param>
        /// <returns></returns>
        public OperatePowerDto[] Gets ( [NumValidate("hr.power.id.error", 1)] long powerId )
        {
            return this._Service.Gets(powerId);
        }
        /// <summary>
        /// 设置操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiPower("all", "hr.power.set")]
        public bool Set ( LongParam<OperatePowerSet> data )
        {
            return this._Service.Set(data.Id, data.Value);
        }

        /// <summary>
        /// 启用操作权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiPower("all", "hr.power.set")]
        public bool SetIsEnable ( LongParam<bool> data )
        {
            return this._Service.SetIsEnable(data.Id, data.Value);
        }
        /// <summary>
        /// 删除操作权限
        /// </summary>
        /// <param name="id"></param>
        [ApiPower("all", "hr.power.delete")]
        public void Delete ( [NumValidate("hr.operate.power.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }
    }
}
