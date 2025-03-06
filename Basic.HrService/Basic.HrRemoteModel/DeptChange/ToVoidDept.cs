using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptChange
{
    [IRemoteConfig("basic.hr.service")]
    public class ToVoidDept : RpcRemote
    {
        public long DeptId
        {
            get;
            set;
        }
    }
}
