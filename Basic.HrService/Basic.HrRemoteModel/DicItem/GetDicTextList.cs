using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDicTextList : RpcRemoteArray<string>
    {
        public long DicId { get; set; }

        public string[] Values { get; set; }
    }
}
