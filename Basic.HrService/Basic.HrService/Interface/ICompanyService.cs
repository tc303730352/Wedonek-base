using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrService.Interface
{
    public interface ICompanyService
    {
        long Add (CompanyAdd add);
        void Delete (long id);
        CompanyDto Get (long id);
        CompanyDto[] Gets (long? parentId, bool isAllChildren);
        CompanyTree[] GetTrees (long? parentId, HrCompanyStatus[] status);
        bool Set (long id, CompanySet set);
    }
}