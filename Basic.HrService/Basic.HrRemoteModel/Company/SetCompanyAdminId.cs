using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class SetCompanyAdminId : RpcRemote<bool>
    {
        public long Id
        {
            get;
            set;
        }
        public long? AdminId
        {
            get;
            set;
        }
    }
}
