using Basic.FormGatewaryModular.Interface;
using Basic.FormGatewaryModular.Model;
using Basic.FormRemoteModel.Table.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Api
{
    internal class TableApi : ApiController
    {
        private readonly ITableService _Service;
        public TableApi ( ITableService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public long Add ( CustomTableAdd datum )
        {
            return this._Service.Add(datum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( [NumValidate("form.table.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomTable Get ( [NumValidate("form.table.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( LongParam<CustomTableSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        public void SetSort ( SetSort[] sort )
        {
            this._Service.SetSort(sort);
        }

    }
}
