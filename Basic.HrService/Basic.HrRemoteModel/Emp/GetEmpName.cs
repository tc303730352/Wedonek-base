using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpName : RpcRemote<string>
    {
        public long Id { get; set; }
    }
}
