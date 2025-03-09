namespace Basic.HrService.Interface
{
    public interface IRoleOperatePowerService
    {
        long[] GetOperateId ( long roleId, long powerId );
        void Set ( long roleId, long powerId, long[] operateId );
    }
}