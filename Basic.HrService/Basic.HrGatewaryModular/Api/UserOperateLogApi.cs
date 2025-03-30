using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class UserOperateLogApi : ApiController
    {
        private readonly IOperateLogService _Service;

        public UserOperateLogApi ( IOperateLogService service )
        {
            this._Service = service;
        }
        public PagingResult<OperateLogDto> Query ( PagingParam<OpLogQueryParam> query )
        {
            return this._Service.Query(query);
        }
        public OperateLogData Get ( [NumValidate("hr.operate.log.id.error", 1)] long id )
        {
            return this._Service.Get(id);
        }
    }
}
