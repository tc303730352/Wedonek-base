using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.DeptChange;
using Basic.HrRemoteModel.DeptChange.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class DeptChangeService : IDeptChangeService
    {

        public void ToVoid (long id)
        {
            new ToVoidDept
            {
                DeptId = id
            }.Send();
        }

        public ChangeDeptTree GetDept (long deptId, bool? isUnit)
        {
            return new GetChangeDept
            {
                DeptId = deptId,
                IsUnit = isUnit
            }.Send();
        }


        public void Merge (long deptId, long toDeptId)
        {
            new MergeDept
            {
                DeptId = deptId,
                ToDeptId = toDeptId
            }.Send();
        }

        public DisbandedDeptEmp[] GetDisbandedEmps (DeptDisbandedArg obj)
        {
            return new GetDisbandedDeptEmp
            {
                Arg = obj
            }.Send();
        }

        public MergeEmp GetMergeEmp (DeptMergeArg arg)
        {
            return new GetMergeDeptEmp
            {
                Arg = arg
            }.Send();
        }
    }
}
