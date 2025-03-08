using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Prower;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class ProwerApi : ApiController
    {
        private readonly IProwerService _Service;
        public ProwerApi ( IProwerService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 添加目录权限
        /// </summary>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        public long Add ( [NullValidate("hr.prower.datum.null")] ProwerAdd datum )
        {
            return this._Service.AddPrower(datum);
        }

        /// <summary>
        /// 获取目录权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public ProwerData Get ( [NumValidate("hr.prower.id.error", 1)] long id )
        {
            return this._Service.GetPrower(id);
        }

        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public ProwerTree[] GetTree ( [NullValidate("hr.prower.param.null")] UI_GetProwerTree param )
        {
            return this._Service.GetProwerTree(param.SubSystemId, param.IsEnable);
        }

        /// <summary>
        /// 获取目录权限树含子系统
        /// </summary>
        /// <returns>目录权限子系统</returns>
        public ProwerSubSystem[] GetTrees ( ProwerGetParam param )
        {
            return this._Service.GetTrees(param);
        }
        /// <summary>
        /// 查询目录权限
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>目录权限</returns>
        public PagingResult<ProwerBase> Query ( [NullValidate("hr.prower.param.null")] PagingParam<ProwerQuery> param )
        {
            ProwerBase[] results = this._Service.QueryPrower(param.Query, param, out int count);
            return new PagingResult<ProwerBase>(count, results);
        }
        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ProwerDataTree[] GetProwerTrees ( ProwerQuery query )
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
        public bool Set ( [NullValidate("hr.prower.param.null")] UI_SetPrower param )
        {
            return this._Service.SetPrower(param.Id, param.Datum);
        }

    }
}
