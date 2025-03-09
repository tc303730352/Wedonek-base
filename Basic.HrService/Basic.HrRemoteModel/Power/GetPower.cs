using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 获取目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetPower : RpcRemote<PowerData>
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public long Id { get; set; }
    }
}
