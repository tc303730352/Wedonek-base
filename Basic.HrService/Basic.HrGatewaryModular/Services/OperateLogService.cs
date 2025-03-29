using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OpLog;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class OperateLogService : IOperateLogService
    {
        public PagingResult<OperateLogDto> Query ( PagingParam<OpLogQueryParam> param )
        {
            return new QueryOperateLog
            {
                Index = param.Index,
                Size = param.Size,
                SortName = param.SortName,
                IsDesc = param.IsDesc,
                NextId = param.NextId,
                Query = param.Query
            }.Send();
        }
    }
}
