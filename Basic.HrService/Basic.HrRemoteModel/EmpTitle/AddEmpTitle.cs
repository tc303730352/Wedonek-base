using Basic.HrRemoteModel.EmpTitle.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpTitle
{
    /// <summary>
    /// 添加人员职务
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddEmpTitle : RpcRemote<long>
    {
        /// <summary>
        /// 职务资料
        /// </summary>
        public EmpTitleAdd Datum { get; set; }
    }
}
