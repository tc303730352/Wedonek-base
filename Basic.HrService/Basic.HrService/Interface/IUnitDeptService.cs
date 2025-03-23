using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;

namespace Basic.HrService.Interface
{
    public interface IUnitDeptService
    {
        DeptSelect[] GetDeptSelect ( DeptSelectGetArg arg );
        DeptTree[] GetTree ( UnitGetArg arg );
        UnitSelect[] GetUnitSelect ( UnitSelectGetParam param );
        CompanyTree<UnitTree> GetUnitTree ( UnitQueryParam param );
    }
}