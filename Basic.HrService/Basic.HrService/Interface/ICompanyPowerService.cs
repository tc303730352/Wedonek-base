namespace Basic.HrService.Interface
{
    public interface ICompanyPowerService
    {
        long[] GetPowerIds ( long companyId );
        bool Sync ( long companyId, long[] powerId );
    }
}