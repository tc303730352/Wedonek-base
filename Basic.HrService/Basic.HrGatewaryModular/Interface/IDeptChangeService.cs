using Basic.HrRemoteModel.DeptChange.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IDeptChangeService
    {
        ChangeDeptTree GetDept (long deptId, bool? isUnit);
        DisbandedDeptEmp[] GetDisbandedEmps (DeptDisbandedArg obj);
        MergeEmp GetMergeEmp (DeptMergeArg arg);
        void Merge (long deptId, long toDeptId);
        void ToVoid (long id);
    }
}