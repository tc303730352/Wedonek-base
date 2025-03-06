using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    /// <summary>
    /// 修改权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetPrower : RpcRemote<bool>
    {
        /// <summary>
        /// 目录权限ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 目录权限资料
        /// </summary>
        public ProwerSet Datum { get; set; }
    }
}
