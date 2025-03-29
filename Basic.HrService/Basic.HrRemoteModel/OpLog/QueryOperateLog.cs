using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpLog
{
    [IRemoteConfig("basic.hr.service")]
    public class QueryOperateLog : BasicPage<OperateLogDto>
    {
        public OpLogQueryParam Query { get; set; }
    }
}
