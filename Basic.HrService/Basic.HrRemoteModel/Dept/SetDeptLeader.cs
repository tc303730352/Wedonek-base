using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    [IRemoteConfig("basic.hr.service")]
    public class SetDeptLeader : RpcRemote
    {
        public long Id
        {
            get;
            set;
        }
        public long? LeaderId
        {
            get;
            set;
        }
    }
}
