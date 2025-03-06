using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpRole
{
    /// <summary>
    /// 获取人员角色列表
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpRole : RpcRemoteArray<long>
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }
    }
}
