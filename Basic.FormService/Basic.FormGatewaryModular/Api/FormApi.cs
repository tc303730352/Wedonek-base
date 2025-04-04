using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Api
{
    internal class FormApi : ApiController
    {
        private readonly IFormService _Service;
        public FormApi ( IFormService service )
        {
            this._Service = service;
        }
        public FormBody GetBody ( [NumValidate("form.id.error", 1)] long id )
        {
            return this._Service.GetBody(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public long Add ( FormAdd datum )
        {
            return this._Service.Add(datum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( [NumValidate("form.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FormDto Get ( [NumValidate("form.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PagingResult<FormDto> Query ( PagingParam<FormQuery> param )
        {
            return this._Service.Query(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( LongParam<FormSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Stop ( [NumValidate("form.id.error", 1)] long id )
        {
            this._Service.Stop(id);
        }
        public void Enable ( [NumValidate("form.id.error", 1)] long id )
        {
            this._Service.Enable(id);
        }
    }
}
