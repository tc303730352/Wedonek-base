using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrCollect
{
    public interface IDeptCollect
    {
        Result[] Gets<Result> (DeptQueryParam query) where Result : class, new();
        DeptBase[] GetUnitDepts (UnitGetParam param);
        string GetDeptName (long id);
        Dictionary<long, string> GetDeptName (long[] ids);
        long Add (DeptAdd add);
        void Delete (DBDept dept);
        bool Enable (DBDept dept);
        bool Enable (long[] deptId);
        DBDept Get (long id);
        long[] GetEnableSubId (DBDept dept);
        long[] GetSubDeptId (long unitId, DBDept dept);
        DeptBase[] GetDepts (DeptGetParam param);
        long[] GetEnableDeptId (long unitId);
        bool Set (DBDept dept, DeptSet set);
        bool Stop (DBDept dept);
        Dictionary<long, string> GetDeptFullName (long[] ids);
        void SetLeader (DBDept dept, long? leaderId);
        long GetUnitId (long deptId);
        KeyValuePair<long, long>[] GetUnitId (long[] deptId);
        void Merge (MergeDept merge);
        void ToVoidDept (long[] deptId);
        string GetUnitDeptName (long id);
    }
}