using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePrower
{
    /// <summary>
    /// 修改操作权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetOperatePrower : RpcRemote<bool>
    {
        public long Id { get; set; }

        public OperateProwerSet Data { get; set; }
    }
}
