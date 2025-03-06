using Basic.HrRemoteModel.Emp;
using Basic.HrRemoteModel.Emp.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class EmpEvent : IRpcApiService
    {
        private readonly IEmpService _Service;

        public EmpEvent (IEmpService service)
        {
            this._Service = service;
        }
        public string GetEmpName (GetEmpName obj)
        {
            return this._Service.GetEmpName(obj.Id);
        }
        public Dictionary<long, string> GetEmpNames (GetEmpNames obj)
        {
            return this._Service.GetEmpName(obj.Ids);
        }
        public void SetEmpEntry (SetEmpEntry obj)
        {
            this._Service.SetEmpEntry(obj.Id, obj.Datum);
        }
        public void SaveEmpHead (SaveEmpHead obj)
        {
            this._Service.SetEmpHead(obj.EmpId, obj.HeadUri, obj.FileId);
        }
        public bool CheckEmpRepeat (CheckEmpRepeat check)
        {
            return this._Service.CheckIsRepeat(check.Check);
        }
        public void SetEmpPost (SetEmpPost obj)
        {
            this._Service.SetEmpPost(obj.EmpId, obj.Post);
        }
        public long AddEmp (AddEmp add)
        {
            return this._Service.Add(add.Datum);
        }

        public void DeleteEmp (DeleteEmp obj)
        {
            this._Service.Delete(obj.Id);
        }
        public EmpBasic GetEmp (GetEmp obj)
        {
            return this._Service.GetBasic(obj.Id);
        }
        public EmpData GetEmpData (GetEmpData obj)
        {
            return this._Service.Get(obj.Id);
        }
        public EmpBasicDatum GetEmpBasic (GetEmpBasic obj)
        {
            return this._Service.GetEmpBasic(obj.Id, obj.CompanyId);
        }
        public EmpBasicDatum[] GetEmpBasics (GetEmpBasics obj)
        {
            return this._Service.GetBasics(obj.EmpId, obj.CompanyId);
        }
        public EmpSelectItem[] GetEmpSelectItem (GetEmpSelectItem param)
        {
            return this._Service.GetSelectItems(param.Param);
        }
        public PagingResult<EmpBasicDto> QueryEmpList (QueryEmpList obj)
        {
            return this._Service.QueryEmp(obj.Query, obj.ToBasicPage());
        }
        public PagingResult<EmpBasicDatum> QueryEmp (QueryEmp obj)
        {
            return this._Service.Query(obj.Query, obj.ToBasicPage());
        }
        public void SetEmpStatus (SetEmpStatus set)
        {
            this._Service.SetEmpStatus(set.Id, set.Status);
        }
        public void SetEmp (SetEmp set)
        {
            this._Service.Set(set.Id, set.Datum);
        }
    }
}
