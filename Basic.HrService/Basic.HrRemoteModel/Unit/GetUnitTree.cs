using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Unit
{
    /// <summary>
    /// 选项单位树形
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetUnitTree : RpcRemoteArray<CompanyTree<UnitTree>>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public UnitQueryParam Param { get; set; }
    }
}
