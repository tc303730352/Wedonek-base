using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrCollect
{
    public interface ICompanyCollect
    {
        bool SetLeaverId ( DBCompany source, long? levelId );
        long Add ( CompanyAdd add );
        void Delete ( DBCompany source );
        DBCompany Get ( long id );
        string GetName ( long companyId );
        Dictionary<long, string> GetNames ( long[] ids );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        T[] Gets<T> ( ComGetParam param ) where T : class, new();
        T[] GetAll<T> () where T : class, new();
        bool SetStatus ( DBCompany source, HrCompanyStatus status );
        bool Set ( DBCompany source, CompanySet set );
    }
}