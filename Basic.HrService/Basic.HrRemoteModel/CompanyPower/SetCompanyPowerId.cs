using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.CompanyPower
{
    [IRemoteConfig("basic.hr.service")]
    public class SetCompanyPowerId : RpcRemote<bool>
    {
        public long CompanyId
        {
            get;
            set;
        }
        public long[] PowerId { get; set; }
    }
}
