using Basic.HrRemoteModel.EmpTitle;
using Basic.HrRemoteModel.EmpTitle.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class EmpTitleEvent : IRpcApiService
    {
        private readonly IEmpTitleService _Service;

        public EmpTitleEvent (IEmpTitleService service)
        {
            this._Service = service;
        }

        public long AddEmpTitle (AddEmpTitle add)
        {
            return this._Service.Add(add.Datum);
        }

        public void DeleteEmpTitle (DeleteEmpTitle obj)
        {
            this._Service.Delete(obj.Id);
        }

        public EmpTitleData GetEmpTitle (GetEmpTitle obj)
        {
            return this._Service.Get(obj.Id);
        }
        public EmpTitleDatum[] GetEmpTitleList (GetEmpTitleList obj)
        {
            return this._Service.Gets(obj.EmpId, obj.CompanyId);
        }
    }
}
