using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 修改部门资料
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetDept : RpcRemote
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 部门资料
        /// </summary>
        public DeptSet Dept { get; set; }
    }
}
