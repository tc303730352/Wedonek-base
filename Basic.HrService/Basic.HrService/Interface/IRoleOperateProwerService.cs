namespace Basic.HrService.Interface
{
    public interface IRoleOperateProwerService
    {
        long[] GetOperateId ( long roleId, long prowerId );
        void Set ( long roleId, long prowerId, long[] operateId );
    }
}