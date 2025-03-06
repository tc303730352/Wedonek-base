using Basic.HrRemoteModel.EmpTitle.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpTitle
{
    /// <summary>
    /// 获取职务列表
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpTitleList : RpcRemoteArray<EmpTitleDatum>
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }
        public long CompanyId { get; set; }
    }
}
