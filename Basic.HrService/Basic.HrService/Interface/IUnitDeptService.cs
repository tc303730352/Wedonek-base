using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;

namespace Basic.HrService.Interface
{
    public interface IUnitDeptService
    {
        DeptSelect[] GetDeptSelect (UnitGetArg arg);
        DeptTree[] GetTree (UnitGetArg arg);
        UnitSelect[] GetUnitSelect (UnitQueryParam param);
        UnitTree[] GetUnitTree (UnitQueryParam param);
    }
}