using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePrower
{
    /// <summary>
    /// 获取启用的操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetsEnableOpPrower : RpcRemoteArray<OperateProwerBase>
    {
        public long ProwerId { get; set; }
    }
}
