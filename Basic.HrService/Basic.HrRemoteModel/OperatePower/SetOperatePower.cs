using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 修改操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetOperatePower : RpcRemote<bool>
    {
        public long Id { get; set; }

        public OperatePowerSet Data { get; set; }
    }
}
