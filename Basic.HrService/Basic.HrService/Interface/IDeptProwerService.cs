namespace Basic.HrService.Interface
{
    public interface IDeptPowerService
    {
        long[] GetDeptPower ( long empId, long companyId );
        void SetPower ( long empId, long companyId, long[] deptId );
    }
}