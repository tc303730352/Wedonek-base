using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Dept
{
    /// <summary>
    /// 获取部门信息
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDept : RpcRemote<DeptDto>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id { get; set; }
    }
}
