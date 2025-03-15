using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.TreeDic
{
    [IRemoteConfig("basic.hr.service")]
    public class GetTreeNames : RpcRemoteArray<string>
    {
        public long DicId
        {
            get;
            set;
        }
        public string[] Values
        {
            get;
            set;
        }
    }
}
