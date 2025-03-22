using Basic.HrRemoteModel.EmpLogin.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpLoginDatumService
    {
        EmpLoginDatum Get ( long empId, long companyId, long[] company );
    }
}