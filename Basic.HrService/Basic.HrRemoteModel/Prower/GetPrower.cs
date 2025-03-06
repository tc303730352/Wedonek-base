using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    /// <summary>
    /// 获取目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetPrower : RpcRemote<ProwerData>
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public long Id { get; set; }
    }
}
