using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptChange
{
    [IRemoteConfig("basic.hr.service")]
    public class MergeDept : RpcRemote
    {
        public long DeptId
        {
            get;
            set;
        }
        public long ToDeptId
        {
            get;
            set;
        }
    }
}
