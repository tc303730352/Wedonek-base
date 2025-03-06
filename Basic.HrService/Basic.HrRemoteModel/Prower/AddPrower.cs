using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    /// <summary>
    /// 添加目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddPrower : RpcRemote<long>
    {
        /// <summary>
        /// 目录权限资料
        /// </summary>
        public ProwerAdd Datum { get; set; }
    }
}
