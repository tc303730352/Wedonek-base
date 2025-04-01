using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.FormGatewaryModular.Api
{
    internal class ControlApi : ApiController
    {
        private readonly IControlService _Service;

        public ControlApi ( IControlService service )
        {
            this._Service = service;
        }

        public long Add ( ControlAdd data )
        {
            return this._Service.Add(data);
        }

        public void Delete ( [NumValidate("form.control.id.error", 1)] long id )
        {
            this._Service.Delete(id);
        }

        public ControlDetailed Get ( [NumValidate("form.control.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }
        public bool Enable ( [NumValidate("form.control.id.error", 1)] long id )
        {
            return this._Service.Enable(id);
        }
        public bool Disable ( [NumValidate("form.control.id.error", 1)] long id )
        {
            return this._Service.Disable(id);
        }

        public PagingResult<ControlDto> Query ( PagingParam<ControlQuery> param )
        {
            return this._Service.Query(param.Query, param);
        }

        public bool Set ( LongParam<ControlSet> param )
        {
            return this._Service.Set(param.Id, param.Value);
        }
    }
}
