using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 添加操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddOperatePower : RpcRemote<long>
    {
        public OperatePowerAdd Data { get; set; }
    }
}
