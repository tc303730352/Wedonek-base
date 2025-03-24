using System.Linq.Expressions;
using Basic.HrModel.Company;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrCollect
{
    public interface ICompanyCollect
    {
        bool SetLeaverId ( DBCompany source, long? empId );
        bool SetAdminId ( DBCompany source, long? empId );
        DBCompany Add ( CompanyAdd add );
        void Delete ( DBCompany source );
        DBCompany Get ( long id );
        Result Get<Result> ( long id ) where Result : class;
        Result Get<Result> ( long id, Expression<Func<DBCompany, Result>> selector );
        string GetName ( long companyId );
        Dictionary<long, string> GetNames ( long[] ids );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        T[] Gets<T> ( ComGetParam param ) where T : class, new();
        T[] GetAll<T> () where T : class, new();
        bool SetStatus ( DBCompany source, HrCompanyStatus status );
        bool Set ( DBCompany source, CompanySet set );
        CompanyName[] GetSubs ( string levelCode );
    }
}