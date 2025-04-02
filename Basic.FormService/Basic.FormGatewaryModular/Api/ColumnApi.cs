using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Column.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Api
{
    internal class ColumnApi : ApiController
    {
        private readonly IColumnService _Service;
        public ColumnApi ( IColumnService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public long Add ( TableColumnAdd datum )
        {
            return this._Service.Add(datum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( [NumValidate("form.column.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TableColumnDto Get ( [NumValidate("form.column.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( LongParam<TableColumnSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool SetGroupId ( LongParam<long> param )
        {
            return this._Service.SetGroupId(param.Id, param.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Service.SetSort(sort);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool SetSpan ( LongParam<int> param )
        {
            return this._Service.SetSpan(param.Id, param.Value);
        }

    }
}
