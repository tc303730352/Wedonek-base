using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Unit
{
    /// <summary>
    /// 获取单位选项数据
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetUnitSelect : RpcRemoteArray<DeptSelect>
    {
        /// <summary>
        /// 参数
        /// </summary>
        public UnitQueryParam Param { get; set; }
    }
}
