using Basic.HrModel.DB;
using Basic.HrModel.EmpTitle;

namespace Basic.HrDAL
{
    public interface IEmpTitleDAL : IBasicDAL<DBEmpTitle, long>
    {
        void Add (DBEmpTitle add);
        void Clear (long empId);
        Result[] GetEmpTitle<Result> (long empId, long companyId) where Result : class, new();
        EmpTitleDto[] GetEmpTitle (long[] empId, long companyId);
        string[] GetTitle (long empId, long deptId);
        long[] GetCompanyIds (long empId);
        Dictionary<long, string[]> GetEmpTitle (KeyValuePair<long, long>[] empAndDept);
    }
}