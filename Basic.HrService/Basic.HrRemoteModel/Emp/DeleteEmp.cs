using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 删除人员
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteEmp : RpcRemote
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public long Id { get; set; }
    }
}
