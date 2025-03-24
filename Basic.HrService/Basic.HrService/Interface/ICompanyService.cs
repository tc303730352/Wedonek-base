using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrService.Interface
{
    public interface ICompanyService
    {
        CompanyTreeItem[] GetTreeItems ();
        bool SetLeaverId ( long id, long? empId );
        bool SetAdminId ( long id, long? empId );
        bool SetStatus ( long id, HrCompanyStatus status );
        string GetName ( long id );
        long Add ( CompanyAdd add );
        void Delete ( long id );
        CompanyDto Get ( long id );
        CompanyDto[] Gets ( long? parentId, bool isAllChildren );
        CompanyTree[] GetTrees ();
        bool Set ( long id, CompanySet set );
    }
}