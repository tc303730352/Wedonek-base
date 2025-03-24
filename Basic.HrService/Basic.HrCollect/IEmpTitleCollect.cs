using System.Linq.Expressions;
using Basic.HrModel.DB;
using Basic.HrModel.EmpTitle;
using Basic.HrRemoteModel.EmpTitle.Model;

namespace Basic.HrCollect
{
    public interface IEmpTitleCollect
    {
        Result[] GetEmpDeptTitle<Result> ( long[] empId, long[] deptId, Expression<Func<DBEmpTitle, Result>> selector );
        long Add ( EmpTitleAdd add );
        void Clear ( long empId );
        void Delete ( DBEmpTitle obj );
        DBEmpTitle Get ( long id );
        long[] GetCompanyIds ( long empId );
        string[] GetTitle ( long empId, long deptId );
        EmpTitleDto[] GetEmpTitle ( long[] empId, long companyId );
        Dictionary<long, string[]> GetEmpDeptTitle ( long[] empId, long deptId );
        Dictionary<long, string[]> GetEmpTitle ( KeyValuePair<long, long>[] empAndDept );
        Result[] GetEmpTitle<Result> ( long empId ) where Result : class, new();
        Result[] GetEmpTitle<Result> ( long empId, long? companyId ) where Result : class, new();
        bool CheckIsExists ( long empId, long comId );
    }
}