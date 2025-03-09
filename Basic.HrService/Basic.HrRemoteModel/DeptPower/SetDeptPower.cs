using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.DeptPower
{
    /// <summary>
    /// 设置员工部门权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetDeptPower : RpcRemote
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 拥有的部门ID
        /// </summary>
        public long[] DeptId { get; set; }
    }
}
