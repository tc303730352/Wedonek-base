using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 查询人员信息
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryEmp : BasicPage<EmpBasicDatum>
    {
        /// <summary>
        /// 人员查询参数
        /// </summary>
        public EmpQuery Query { get; set; }
    }
}
