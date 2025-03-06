namespace Basic.HrService.Interface
{
    public interface IDeptProwerService
    {
        long[] GetDeptPrower (long empId, long companyId);
        void SetPrower (long empId, long companyId, long[] deptId);
    }
}