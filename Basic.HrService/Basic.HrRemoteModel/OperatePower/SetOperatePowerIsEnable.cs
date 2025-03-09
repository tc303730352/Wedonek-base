using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.OperatePower
{
    /// <summary>
    /// 修改操作权限是否启用
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetOperatePowerIsEnable : RpcRemote<bool>
    {
        /// <summary>
        /// 操作权限ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
