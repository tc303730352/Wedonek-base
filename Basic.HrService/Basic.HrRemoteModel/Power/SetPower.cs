using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    /// <summary>
    /// 修改权限
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class SetPower : RpcRemote<bool>
    {
        /// <summary>
        /// 目录权限ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 目录权限资料
        /// </summary>
        public PowerSet Datum { get; set; }
    }
}
