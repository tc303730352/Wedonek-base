using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.TableGroup.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Api
{
    internal class TableGroupApi : ApiController
    {
        private readonly ITableGroupService _Service;
        public TableGroupApi ( ITableGroupService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public long Add ( TableGroupAdd datum )
        {
            return this._Service.Add(datum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( [NumValidate("form.group.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TableGroupDto Get ( [NumValidate("form.group.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( LongParam<TableGroupSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        public void SetSort ( LongNullParam<int>[] sort )
        {
            this._Service.SetSort(sort);
        }

    }
}
