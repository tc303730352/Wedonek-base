using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 修改人员资料
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetEmp : RpcRemote
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 人员资料
        /// </summary>
        public EmpSet Datum { get; set; }
    }
}
