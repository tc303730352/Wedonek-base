using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 新建人员
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddEmp : RpcRemote<long>
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        public EmpAdd Datum { get; set; }
    }
}
