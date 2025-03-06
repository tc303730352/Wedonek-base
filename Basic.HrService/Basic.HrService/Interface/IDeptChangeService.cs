using Basic.HrRemoteModel.DeptChange.Model;

namespace Basic.HrService.Interface
{
    public interface IDeptChangeService
    {
        void ToVoid (long deptId);
        ChangeDeptTree GetDept (long deptId, bool? isUnit);
        MergeEmp GetMergeEmp (DeptMergeArg arg);
        DisbandedDeptEmp[] GetDisbandedDeptEmp (DeptDisbandedArg arg);
        void MergeDept (long deptId, long toDeptId);
    }
}