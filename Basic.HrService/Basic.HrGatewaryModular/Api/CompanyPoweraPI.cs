using Basic.HrGatewaryModular.Interface;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class CompanyPowerApi : ApiController
    {
        private readonly ICompanyPowerService _Service;

        public CompanyPowerApi ( ICompanyPowerService service )
        {
            this._Service = service;
        }

        public long[] Gets ( [NumValidate("hr.company.id.error", 1)] long companyId )
        {
            return this._Service.GetPowerIds(companyId);
        }

        public bool Sync ( LongParam<long[]> param )
        {
            return this._Service.Sync(param.Id, param.Value);
        }
    }
}
