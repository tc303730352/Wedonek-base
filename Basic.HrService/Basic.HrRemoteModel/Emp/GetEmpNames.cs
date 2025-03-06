using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpNames : RpcRemote<Dictionary<long, string>>
    {
        public long[] Ids { get; set; }
    }
}
