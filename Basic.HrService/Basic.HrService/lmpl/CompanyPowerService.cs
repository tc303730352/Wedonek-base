using Basic.HrCollect;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class CompanyPowerService : ICompanyPowerService
    {
        private readonly ICompanyPowerCollect _ComPower;

        public CompanyPowerService ( ICompanyPowerCollect comPower )
        {
            this._ComPower = comPower;
        }

        public long[] GetPowerIds ( long companyId )
        {
            return this._ComPower.GetPowerIds(companyId);
        }

        public bool Sync ( long companyId, long[] powerId )
        {
            return this._ComPower.Sync(companyId, powerId);
        }
    }
}
