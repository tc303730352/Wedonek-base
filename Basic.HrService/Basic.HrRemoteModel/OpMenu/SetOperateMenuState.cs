using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpMenu
{
    [IRemoteConfig("basic.hr.service")]
    public class SetOperateMenuState : RpcRemote<bool>
    {
        public long Id
        {
            get;
            set;
        }

        public bool IsEnable { get; set; }
    }
}
