using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 获取目录权限树含子系统
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetPowerTreeBySystem : RpcRemoteArray<PowerSubSystem>
    {
        /// <summary>
        /// 权限类型
        /// </summary>
        public PowerGetParam Param { get; set; }
    }
}
