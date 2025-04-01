using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DicItem
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDictItemNames : RpcRemoteArray<DicItemName>
    {
        public long[] DicId
        {
            get;
            set;
        }
    }
}
