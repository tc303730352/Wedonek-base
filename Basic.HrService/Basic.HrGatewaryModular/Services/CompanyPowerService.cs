using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.CompanyPower;

namespace Basic.HrGatewaryModular.Services
{
    internal class CompanyPowerService : ICompanyPowerService
    {
        public long[] GetPowerIds ( long companyId )
        {
            return new GetCompanyPowerId
            {
                CompanyId = companyId,
            }.Send();
        }

        public bool Sync ( long companyId, long[] powerId )
        {
            return new SetCompanyPowerId
            {
                CompanyId = companyId,
                PowerId = powerId
            }.Send();
        }
    }
}
