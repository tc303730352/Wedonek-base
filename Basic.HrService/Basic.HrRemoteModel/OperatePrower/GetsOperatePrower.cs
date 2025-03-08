using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePrower
{
    /// <summary>
    /// 获取操作权限集
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetsOperatePrower : RpcRemoteArray<OperateProwerDto>
    {
        public long ProwerId { get; set; }
    }
}
