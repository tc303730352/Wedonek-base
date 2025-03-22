namespace Basic.HrCollect
{
    public interface ICompanyPowerCollect
    {
        void Clear ( long companyId );
        void ClearByPower ( long powerId );
        long[] GetPowerIds ( long companyId );
        bool Sync ( long companyId, long[] powerId );
    }
}