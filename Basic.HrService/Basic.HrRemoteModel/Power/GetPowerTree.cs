using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 获取目录权限树
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetPowerTree : RpcRemoteArray<PowerTree>
    {
        /// <summary>
        /// 子级系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}
