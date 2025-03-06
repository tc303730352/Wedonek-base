using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDicItem : RpcRemote<DicItemDto>
    {
        public long Id { get; set; }
    }
}
