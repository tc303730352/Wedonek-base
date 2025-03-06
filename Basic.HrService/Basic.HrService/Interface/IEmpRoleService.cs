namespace Basic.HrService.Interface
{
    public interface IEmpRoleService
    {
        long[] GetRoleId (long empId);
        void SetRole (long empId, long[] roleId);
    }
}