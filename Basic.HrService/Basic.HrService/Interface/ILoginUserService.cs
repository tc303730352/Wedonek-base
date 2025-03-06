namespace Basic.HrService.Interface
{
    public interface ILoginUserService
    {
        void Delete (long empId);
        void Open (long[] empId, long companyId);
    }
}