using Basic.HrRemoteModel.CompanyPower;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class CompanyPowerEvent : IRpcApiService
    {
        private readonly ICompanyPowerService _Service;

        public CompanyPowerEvent ( ICompanyPowerService service )
        {
            this._Service = service;
        }

        public bool SetCompanyPowerId ( SetCompanyPowerId obj )
        {
            return this._Service.Sync(obj.CompanyId, obj.PowerId);
        }
        public long[] GetCompanyPowerId ( GetCompanyPowerId obj )
        {
            return this._Service.GetPowerIds(obj.CompanyId);
        }
    }
}
