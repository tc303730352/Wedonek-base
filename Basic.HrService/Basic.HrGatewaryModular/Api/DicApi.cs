using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Dic;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class DicApi : ApiController
    {
        private readonly IDicService _Service;
        public DicApi ( IDicService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 新建字典
        /// </summary>
        /// <param name="datum">字典资料</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.add")]
        public long Add ( [NullValidate("hr.dic.datum.null")] DicAdd datum )
        {
            return this._Service.AddDic(datum);
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id">字典ID</param>
        [ApiPower("all", "hr.dic.delete")]
        public void Delete ( [NumValidate("hr.dic.id.error", 1)] long id )
        {
            this._Service.DeleteDic(id);
        }

        /// <summary>
        /// 启用字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Enable ( [NumValidate("hr.dic.id.error", 1)] long id )
        {
            return this._Service.EnableDic(id);
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        public DicDto Get ( [NumValidate("hr.dic.id.error", 1)] long id )
        {
            return this._Service.GetDic(id);
        }

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PagingResult<DicDatum> Query ( [NullValidate("hr.dic.param.null")] UI_QueryDic param )
        {
            DicDatum[] results = this._Service.QueryDic(param.Query, param, out int count);
            return new PagingResult<DicDatum>(count, results);
        }

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Set ( [NullValidate("hr.dic.param.null")] UI_SetDic param )
        {
            return this._Service.SetDic(param.Id, param.Datum);
        }

        /// <summary>
        /// 停用字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Stop ( [NumValidate("hr.dic.id.error", 1)] long id )
        {
            return this._Service.StopDic(id);
        }

    }
}
