using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class SetCompanyLeaverId : RpcRemote<bool>
    {
        public long Id
        {
            get;
            set;
        }
        public long? LeaverId
        {
            get;
            set;
        }
    }
}
