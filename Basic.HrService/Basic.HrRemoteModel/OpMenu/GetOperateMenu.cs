using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpMenu
{
    [IRemoteConfig("basic.hr.service")]
    public class GetOperateMenu : RpcRemote<OperateMenuDto>
    {
        public long Id { get; set; }
    }
}
