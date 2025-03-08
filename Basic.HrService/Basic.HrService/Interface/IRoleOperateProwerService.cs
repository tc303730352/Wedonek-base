namespace Basic.HrService.Interface
{
    public interface IRoleOperateProwerService
    {
        long[] GetProwerId ( long roleId );
        string[] GetProwerVal ( long roleId );
        void Set ( long roleId, long[] prowerId );
    }
}