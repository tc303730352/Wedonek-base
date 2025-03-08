using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IRoleProwerService
    {
        long[] GetProwerId ( long roleId );

        void Set ( long roleId, long[] prowerId );

        OperateProwerDto[] Gets ( long prowerId );
    }
}