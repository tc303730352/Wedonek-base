using Basic.HrRemoteModel.EmpTitle.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpTitle
{
    /// <summary>
    /// 获取职务
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpTitle : RpcRemote<EmpTitleData>
    {
        /// <summary>
        /// 职务ID
        /// </summary>
        public long Id { get; set; }
    }
}
