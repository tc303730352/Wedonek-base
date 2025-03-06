using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.EmpTitle
{
    /// <summary>
    /// 删除人员职务
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteEmpTitle : RpcRemote
    {
        /// <summary>
        /// 职务ID
        /// </summary>
        public long Id { get; set; }
    }
}
