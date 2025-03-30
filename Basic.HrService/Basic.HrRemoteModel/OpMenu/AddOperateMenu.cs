using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OpMenu
{
    [IRemoteConfig("basic.hr.service")]
    public class AddOperateMenu : RpcRemote<long>
    {
        public OpMenuAdd Datum
        {
            get;
            set;
        }
    }
}
