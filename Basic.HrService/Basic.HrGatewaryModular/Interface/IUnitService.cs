using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IUnitService
    {
        string[] GetName ( long[] id );
        /// <summary>
        /// 获取独立单位
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        DeptSelect[] GetUnitDeptSelect ( DeptSelectGetArg param );

        /// <summary>
        /// 获取独立机构树
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns>部门树</returns>
        CompanyTree<DeptTree>[] GetUnitDeptTree ( UnitGetArg param );

        /// <summary>
        /// 获取单位选项数据
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        DeptSelect[] GetUnitSelect ( UnitSelectGetParam param );

        /// <summary>
        /// 选项单位树形
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns>部门树</returns>
        CompanyTree<UnitTree>[] GetUnitTree ( UnitQueryParam param );

    }
}
