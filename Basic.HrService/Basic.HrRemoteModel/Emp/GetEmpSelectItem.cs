using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 获取人员选择项
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpSelectItem : RpcRemoteArray<EmpSelectItem>
    {
        /// <summary>
        /// 选择项资料
        /// </summary>
        public SelectGetParam Param { get; set; }
    }
}
