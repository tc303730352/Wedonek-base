using Basic.HrRemoteModel.DeptChange.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptChange
{
    [IRemoteConfig("basic.hr.service")]
    public class GetChangeDept : RpcRemote<ChangeDeptTree>
    {
        public long DeptId
        {
            get;
            set;
        }
        public bool? IsUnit
        {
            get;
            set;
        }
    }
}
