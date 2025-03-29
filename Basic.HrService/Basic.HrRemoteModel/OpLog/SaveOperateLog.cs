using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpLog
{
    [IRemoteConfig("basic.hr.service", IsReply = false)]
    public class SaveOperateLog : RpcRemote
    {
        public OperateLogAdd[] Logs { get; set; }
    }
}
