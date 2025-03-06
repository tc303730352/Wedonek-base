using System.Collections.Frozen;
using Basic.HrRemoteModel.EmpLogin.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpLoginDatumService
    {
        EmpLoginDatum Get (long empId);
        Dictionary<long, string> GetEmpCompany (long empId);
    }
}