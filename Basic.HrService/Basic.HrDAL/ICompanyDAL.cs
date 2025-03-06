using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrDAL
{
    public interface ICompanyDAL : IBasicDAL<DBCompany, long>
    {
        Result[] Gets<Result> (ComGetParam param) where Result : class, new();
        long Add (DBCompany company);
        bool CheckFullName (string name);
        bool CheckShortName (string name);
        void SetLeaverId (long id, long? leaverId);
        void SetStatus (long id, HrCompanyStatus status);
        Dictionary<long, string> GetNames (long[] ids);
        string GetName (long companyId);
    }
}