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

        public DeptSelect[] GetDeptSelect ( DeptGetArg getParam )
        {
            return new GetDeptSelect
            {
                GetParam = getParam,
            }.Send();
        }

        public DeptTree[] GetDeptTree ( DeptGetArg param )
        {
            return new GetDeptTree
            {
                Param = param,
            }.Send();
        }

        public DeptFullTree[] Gets ( DeptQueryParam obj )
        {
            return new GetDeptList
            {
                Query = obj
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
