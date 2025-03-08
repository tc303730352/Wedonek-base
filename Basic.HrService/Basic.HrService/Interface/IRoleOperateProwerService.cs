namespace Basic.HrService.Interface
{
    public interface IRoleOperateProwerService
    {
        long[] GetOperateId ( long roleId, long prowerId );
        string[] GetOperateVal ( long roleId );
        void Set ( long roleId, long[] prowerId );
    }
}