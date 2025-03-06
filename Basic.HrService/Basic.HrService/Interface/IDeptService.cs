using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrService.Interface
{
    public interface IDeptService
    {
        DeptSelect[] GetDeptSelect (DeptGetArg arg);
        long Add (DeptAdd add);
        void Delete (long id);
        bool Enable (long[] deptId);
        DeptDto Get (long id);
        DeptTree[] GetTree (DeptGetArg arg);
        bool Set (long id, DeptSet set);
        bool Stop (long deptId);
        DeptFullTree[] GetDeptList (DeptQueryParam query);
        void SetLeader (long id, long? leaderId);
    }
}