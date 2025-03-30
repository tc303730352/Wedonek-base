using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateLogService
    {
        OperateLogData Get ( long id );
        PagingResult<OperateLogDto> Query ( PagingParam<OpLogQueryParam> param );
    }
}