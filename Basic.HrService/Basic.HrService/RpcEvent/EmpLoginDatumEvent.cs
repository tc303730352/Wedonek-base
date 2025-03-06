using Basic.HrRemoteModel.EmpLogin;
using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class EmpLoginDatumEvent : IRpcApiService
    {
        private readonly IEmpLoginDatumService _Service;

        public EmpLoginDatumEvent (IEmpLoginDatumService service)
        {
            this._Service = service;
        }

        public EmpLoginDatum GetEmpLoginDatum (GetEmpLoginDatum obj)
        {
            return this._Service.Get(obj.EmpId);
        }
    }
}
