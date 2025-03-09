using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Power
{
    [IRemoteConfig("basic.hr.service")]
    public class GetPowerTrees : RpcRemoteArray<PowerDataTree>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public PowerQuery Query { get; set; }
    }
}
