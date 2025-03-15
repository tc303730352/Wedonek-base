using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    [IRemoteConfig("basic.hr.service")]
    public class GetDeptNameList : RpcRemoteArray<string>
    {
        public long[] Id { get; set; }
    }
}
