using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class SetEmpStatus : RpcRemote
    {
        public long Id
        {
            get;
            set;
        }
        public HrEmpStatus Status { get; set; }
    }
}
