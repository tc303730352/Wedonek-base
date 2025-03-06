using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Unit
{
    /// <summary>
    /// 获取独立单位
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetUnitDeptSelect : RpcRemoteArray<DeptSelect>
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public UnitGetArg Param { get; set; }
    }
}
