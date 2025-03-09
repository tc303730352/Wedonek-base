using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Power;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class PowerApi : ApiController
    {
        private readonly IPowerService _Service;
        public PowerApi ( IPowerService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 添加目录权限
        /// </summary>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        public long Add ( [NullValidate("hr.power.datum.null")] PowerAdd datum )
        {
            return this._Service.AddPower(datum);
        }

        /// <summary>
        /// 获取目录权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public PowerData Get ( [NumValidate("hr.power.id.error", 1)] long id )
        {
            return this._Service.GetPower(id);
        }

        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PowerTree[] GetTree ( [NullValidate("hr.power.param.null")] UI_GetPowerTree param )
        {
            return this._Service.GetPowerTree(param.SubSystemId, param.IsEnable);
        }

        /// <summary>
        /// 获取目录权限树含子系统
        /// </summary>
        /// <returns>目录权限子系统</returns>
        public PowerSubSystem[] GetTrees ( PowerGetParam param )
        {
            return this._Service.GetTrees(param);
        }
        /// <summary>
        /// 查询目录权限
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>目录权限</returns>
        public PagingResult<PowerBase> Query ( [NullValidate("hr.power.param.null")] PagingParam<PowerQuery> param )
        {
            PowerBase[] results = this._Service.QueryPower(param.Query, param, out int count);
            return new PagingResult<PowerBase>(count, results);
        }
        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PowerDataTree[] GetPowerTrees ( PowerQuery query )
        {
            return this._Service.GetTrees(query);
        }
        /// <summary>
        /// 设置排序位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SetSort ( LongParam<int> param )
        {
            return this._Service.SetSort(param.Id, param.Value);
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( [NullValidate("hr.power.param.null")] LongParam<PowerSet> param )
        {
            return this._Service.SetPower(param.Id, param.Value);
        }

    }
}
