using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 获取人员信息
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetEmp : RpcRemote<EmpBasic>
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long Id { get; set; }
    }
}
