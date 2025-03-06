using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    [IRemoteConfig("basic.hr.service")]
    public class MoveDicItem : RpcRemote
    {
        public long Id
        {
            get;
            set;
        }
        public long ToId { get; set; }
    }
}
