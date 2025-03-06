using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class SaveEmpHead : RpcRemote
    {
        public long EmpId
        {
            get;
            set;
        }
        public string HeadUri
        {
            get;
            set;
        }
        public long FileId { get; set; }
    }
}
