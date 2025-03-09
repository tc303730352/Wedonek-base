using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 删除操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class DeleteOperatePower : RpcRemote
    {
        public long Id { get; set; }
    }
}
