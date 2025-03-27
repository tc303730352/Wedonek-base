using Basic.HrRemoteModel.EmpTitle.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpTitleService
    {
        long Add ( EmpTitleAdd add );
        void Delete ( long id );
        EmpTitleData Get ( long id );
        string[] GetDeptTitle ( long empId, long deptId );
        EmpTitleDatum[] Gets ( long empId, long companyId );
    }
}