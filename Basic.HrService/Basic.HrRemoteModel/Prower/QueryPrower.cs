using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    /// <summary>
    /// 查询目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class QueryPrower : BasicPage<ProwerBase>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public ProwerQuery QueryParam { get; set; }
    }
}
