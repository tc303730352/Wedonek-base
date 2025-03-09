using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 添加目录权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class AddPower : RpcRemote<long>
    {
        /// <summary>
        /// 目录权限资料
        /// </summary>
        public PowerAdd Datum { get; set; }
    }
}
