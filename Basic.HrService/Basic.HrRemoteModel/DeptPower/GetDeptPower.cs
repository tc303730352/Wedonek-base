using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptPower
{
    /// <summary>
    /// 获取员工部门数据权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetDeptPower : RpcRemoteArray<long>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId { get; set; }

        public long CompanyId { get; set; }
    }
}
