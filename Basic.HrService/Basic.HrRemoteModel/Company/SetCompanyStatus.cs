using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class SetCompanyStatus : RpcRemote<bool>
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }

        public HrCompanyStatus Status { get; set; }
    }
}
