using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePrower
{
    /// <summary>
    /// 添加操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddOperatePrower : RpcRemote<long>
    {
        public OperateProwerAdd Data { get; set; }
    }
}
