using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Dept;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit;
using Basic.HrRemoteModel.Unit.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class UnitService : IUnitService
    {
        public DeptSelect[] GetUnitDeptSelect ( UnitGetArg param )
        {
            if ( param.DeptId == null || param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptSelect>();
            }
            return new GetUnitDeptSelect
            {
                Param = param,
            }.Send();
        }

        public DeptTree[] GetUnitDeptTree ( UnitGetArg param )
        {
            if ( param.DeptId == null || param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptTree>();
            }
            return new GetUnitDeptTree
            {
                Param = param,
            }.Send();
        }
        public string[] GetName ( long[] id )
        {
            return new GetDeptNameList
            {
                Id = id
            }.Send();
        }
        public DeptSelect[] GetUnitSelect ( UnitQueryParam param )
        {
            if ( param.DeptId == null || param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptSelect>();
            }
            return new GetUnitSelect
            {
                Param = param,
            }.Send();
        }

        public DeptTree[] GetUnitTree ( UnitQueryParam param )
        {
            if ( param.DeptId == null || param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptTree>();
            }
            return new GetUnitTree
            {
                Param = param,
            }.Send();
        }

    }
}
