using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpMenu
{
    [IRemoteConfig("basic.hr.service")]
    public class DeleteOperateMenu : RpcRemote
    {
        public long Id { get; set; }
    }
}
