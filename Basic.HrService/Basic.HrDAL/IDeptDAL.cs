using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrDAL
{
    public interface IDeptDAL : IBasicDAL<DBDept, long>
    {
        long[] GetEnableDeptId (long unitId);
        Result[] Gets<Result> (DeptQueryParam query) where Result : class, new();
        string GetDeptName (long id);
        Dictionary<long, string> GetDeptName (long[] ids);
        void AddDept (DBDept dept);
        bool CheckRepeat (long companyId, long parentId, string deptName);
        bool CheckShortRepeat (long companyId, long parentId, string shortName);
        int GetMaxSort (long companyId, long parentId);
        long[] GetsSubId (DBDept dept);
        void Delete (long[] ids);
        long[] GetSubDeptId (long unitId, DBDept dept);
        void EnableDept (DBDept dept);
        void EnableDept (long[] ids);
        long[] GetsSubId (DBDept dept, HrDeptStatus status);
        void StopDept (long[] ids);
        T[] GetDepts<T> (DeptGetParam param) where T : class, new();
        bool Set (DBDept dept, DeptSet set);
        T[] GetUnitDepts<T> (UnitGetParam param) where T : class, new();
        void SetLeader (DBDept dept, long? leaderId);
        void Set (DBDept dept, DeptSetArg arg, SubDeptSet[] sets);
        void Merge (MergeDept merge, MergeSubDeptSet[]? subs);
        void ToVoidDept (long[] deptId);
        string GetUnitDeptName (long id);
    }
}