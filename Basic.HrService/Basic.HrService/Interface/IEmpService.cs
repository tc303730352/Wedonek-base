using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IEmpService
    {
        string GetEmpName (long id);

        Dictionary<long, string> GetEmpName (long[] ids);
        bool CheckIsRepeat (DataRepeatCheck check);
        long Add (EmpAdd add);
        void Delete (long id);
        EmpData Get (long id);
        EmpBasic GetBasic (long id);
        EmpSelectItem[] GetSelectItems (SelectGetParam param);
        PagingResult<EmpBasicDatum> Query (EmpQuery query, IBasicPage paging);
        void Set (long id, EmpSet set);
        EmpBasicDatum[] GetBasics (long[] empId, long companyId);
        EmpBasicDatum GetEmpBasic (long id, long companyId);
        PagingResult<EmpBasicDto> QueryEmp (EmpQuery query, IBasicPage paging);
        void SetEmpStatus (long id, HrEmpStatus status);
        void SetEmpHead (long empId, string headUri, long fileId);
        void SetEmpPost (long empId, string post);
        void SetEmpEntry (long id, EmpEntry datum);
    }
}