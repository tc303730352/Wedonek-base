using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 删除权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeletePower : RpcRemote
    {
        public long Id { get; set; }
    }
}
