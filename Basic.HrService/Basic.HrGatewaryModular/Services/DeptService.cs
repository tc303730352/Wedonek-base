using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Dept;
using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class DeptService : IDeptService
    {
        public long AddDept ( DeptAdd datum )
        {
            return new AddDept
            {
                Datum = datum,
            }.Send();
        }
        public DeptTallyTree[] GetTallyTrees ( DeptGetArg param )
        {
            if ( param.DeptId != null && param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptTallyTree>();
            }
            return new GetTallyTrees
            {
                Param = param
            }.Send();
        }
        public void DeleteDept ( long id )
        {
            new DeleteDept
            {
                Id = id,
            }.Send();
        }

        public bool EnableDept ( long[] deptId )
        {
            return new EnableDept
            {
                DeptId = deptId,
            }.Send();
        }

        public DeptDto GetDept ( long id )
        {
            return new GetDept
            {
                Id = id,
            }.Send();
        }

        public DeptSelect[] GetDeptSelect ( DeptGetArg param )
        {
            if ( param.DeptId != null && param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptSelect>();
            }
            return new GetDeptSelect
            {
                GetParam = param,
            }.Send();
        }

        public DeptTree[] GetDeptTree ( DeptGetArg param )
        {
            if ( param.DeptId != null && param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptTree>();
            }
            return new GetDeptTree
            {
                Param = param,
            }.Send();
        }

        public DeptFullTree[] Gets ( DeptQueryParam param )
        {
            if ( param.DeptId != null && param.DeptId.Length == 0 )
            {
                return Array.Empty<DeptFullTree>();
            }
            return new GetDeptList
            {
                Query = param
            }.Send();
        }

        public void SetDept ( long id, DeptSet dept )
        {
            new SetDept
            {
                Id = id,
                Dept = dept,
            }.Send();
        }

        public void SetLeader ( long id, long? leaderId )
        {
            new SetDeptLeader
            {
                Id = id,
                LeaderId = leaderId
            }.Send();
        }

        public bool StopDept ( long deptId )
        {
            return new StopDept
            {
                DeptId = deptId,
            }.Send();
        }
    }
}
