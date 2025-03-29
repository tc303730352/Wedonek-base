using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
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
    }
}
