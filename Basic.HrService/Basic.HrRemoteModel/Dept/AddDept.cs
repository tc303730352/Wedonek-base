using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 添加部门
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddDept : RpcRemote<long>
    {
        /// <summary>
        /// 部门资料
        /// </summary>
        public DeptAdd Datum { get; set; }
    }
}
