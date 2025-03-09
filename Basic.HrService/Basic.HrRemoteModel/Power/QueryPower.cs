using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 查询目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryPower : BasicPage<PowerBase>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public PowerQuery QueryParam { get; set; }
    }
}
