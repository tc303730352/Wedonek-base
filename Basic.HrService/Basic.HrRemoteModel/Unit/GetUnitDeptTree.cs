using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Unit
{
    /// <summary>
    /// 获取独立机构树
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetUnitDeptTree : RpcRemoteArray<CompanyTree<DeptTree>>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public UnitGetArg Param { get; set; }
    }
}
