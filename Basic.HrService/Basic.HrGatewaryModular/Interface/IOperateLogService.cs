using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateLogService
    {
        PagingResult<OperateLogDto> Query ( PagingParam<OpLogQueryParam> param );
    }
}