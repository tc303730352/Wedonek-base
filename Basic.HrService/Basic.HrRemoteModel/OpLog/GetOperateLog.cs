using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpLog
{
    [IRemoteConfig("basic.hr.service")]
    public class GetOperateLog : RpcRemote<OperateLogData>
    {
        public long Id { get; set; }
    }
}
