using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    [IRemoteConfig("basic.hr.service")]
    public class GetProwerTrees : RpcRemoteArray<ProwerDataTree>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public ProwerQuery Query { get; set; }
    }
}
