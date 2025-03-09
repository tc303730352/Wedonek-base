using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 获取启用的操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetsEnableOpPower : RpcRemoteArray<OperatePowerBase>
    {
        public long PowerId { get; set; }
    }
}
