using Basic.HrCollect;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class CompanyPowerService : ICompanyPowerService
    {
        private readonly ICompanyPowerCollect _ComPower;
        private readonly IPowerCollect _Power;
        public CompanyPowerService ( ICompanyPowerCollect comPower, IPowerCollect power )
        {
            this._Power = power;
            this._ComPower = comPower;
        }

        public long[] GetPowerIds ( long companyId )
        {
            return this._ComPower.GetPowerIds(companyId);
        }

        public bool Sync ( long companyId, long[] powerId )
        {
            powerId = this._Power.Filters(powerId, HrRemoteModel.PowerType.menu);
            return this._ComPower.Sync(companyId, powerId);
        }
    }
}
