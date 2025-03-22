namespace Basic.HrService.Interface
{
    public interface IEmpRoleService
    {
        long[] GetRoleId ( long empId, long companyId );
        void SetRole ( long empId, long companyId, long[] roleId );
    }
}