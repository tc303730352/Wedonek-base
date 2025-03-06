using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class QueryEmpList : BasicPage<EmpBasicDto>
    {
        /// <summary>
        /// 人员查询参数
        /// </summary>
        public EmpQuery Query { get; set; }
    }
}
