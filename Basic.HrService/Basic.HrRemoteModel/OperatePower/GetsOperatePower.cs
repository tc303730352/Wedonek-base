using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 获取操作权限集
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetsOperatePower : RpcRemoteArray<OperatePowerDto>
    {
        public long PowerId { get; set; }
    }
}
