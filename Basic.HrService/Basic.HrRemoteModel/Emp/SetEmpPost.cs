using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class SetEmpPost : RpcRemote
    {
        public long EmpId
        {
            get;
            set;
        }
        public string Post
        {
            get;
            set;
        }
    }
}
