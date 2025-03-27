using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class TreeDicApi : ApiController
    {
        private readonly ITreeDicService _Service;
        public TreeDicApi ( ITreeDicService service )
        {
            this._Service = service;
        }
        public TreeFullItem[] GetTree ( TreeItemQuery query )
        {
            return this._Service.GetTree(query);
        }
        /// <summary>
        /// 获取树形字典项名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string[] GetNames ( LongParam<string[]> param )
        {
            return this._Service.GetTreeNames(param.Id, param.Value);
        }

        /// <summary>
        /// 添加树形字典项
        /// </summary>
        /// <param name="datum">树形字典资料</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.add")]
        public long Add ( [NullValidate("hr.treedic.datum.null")] TreeDicItemAdd datum )
        {
            return this._Service.AddTreeDicItem(datum);
        }

        /// <summary>
        /// 删除树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        [ApiPower("all", "hr.dic.delete")]
        public void Delete ( [NumValidate("hr.treedic.id.error", 1)] long id )
        {
            this._Service.DeleteTreeDicItem(id);
        }

        /// <summary>
        /// 启用树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Enable ( [NumValidate("hr.treedic.id.error", 1)] long id )
        {
            return this._Service.EnableTreeDicItem(id);
        }

        /// <summary>
        /// 获取树形字典项
        /// </summary>
        /// <param name="dicId">字典ID</param>
        /// <returns>树形字典</returns>
        public TreeItemBase[] GetDicTree ( [NumValidate("hr.treedic.dicId.error", 1)] long dicId )
        {
            return this._Service.GetDicTree(dicId);
        }

        /// <summary>
        /// 获取树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        /// <returns></returns>
        public TreeDicItemDto Get ( [NumValidate("hr.treedic.id.error", 1)] long id )
        {
            return this._Service.GetTreeDicItem(id);
        }

        /// <summary>
        /// 移动树形字典
        /// </summary>
        /// <param name="param">参数</param>
        [ApiPower("all", "hr.dic.set")]
        public void Move ( [NullValidate("hr.treedic.param.null")] UI_MoveTreeDicItem param )
        {
            this._Service.MoveTreeDicItem(param.FromId, param.ToId);
        }

        /// <summary>
        /// 查询字典项
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PagingResult<TreeDicItemDto> Query ( [NullValidate("hr.treedic.param.null")] UI_QueryTreeDicItem param )
        {
            TreeDicItemDto[] results = this._Service.QueryTreeDicItem(param.Query, param, out int count);
            return new PagingResult<TreeDicItemDto>(count, results);
        }

        /// <summary>
        /// 修改树形字典项
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Set ( [NullValidate("hr.treedic.param.null")] UI_SetTreeDicItem param )
        {
            return this._Service.SetTreeDicItem(param.Id, param.Datum);
        }

        /// <summary>
        /// 停用树形字典项
        /// </summary>
        /// <param name="id">树形字典ID</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Stop ( [NumValidate("hr.treedic.id.error", 1)] long id )
        {
            return this._Service.StopTreeDicItem(id);
        }

    }
}
