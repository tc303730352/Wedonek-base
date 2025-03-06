using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit;
using Basic.HrRemoteModel.Unit.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class UnitService : IUnitService
    {
        public DeptSelect[] GetUnitDeptSelect (UnitGetArg param)
        {
            return new GetUnitDeptSelect
            {
                Param = param,
            }.Send();
        }

        public DeptTree[] GetUnitDeptTree (UnitGetArg param)
        {
            return new GetUnitDeptTree
            {
                Param = param,
            }.Send();
        }

        public DeptSelect[] GetUnitSelect (UnitQueryParam param)
        {
            return new GetUnitSelect
            {
                Param = param,
            }.Send();
        }

        public DeptTree[] GetUnitTree (UnitQueryParam param)
        {
            return new GetUnitTree
            {
                Param = param,
            }.Send();
        }

    }
}
