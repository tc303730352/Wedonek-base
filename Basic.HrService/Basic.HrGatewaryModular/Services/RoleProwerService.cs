using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OperatePrower;
using Basic.HrRemoteModel.OperatePrower.Model;
using Basic.HrRemoteModel.RolePrower;

namespace Basic.HrGatewaryModular.Services
{
    internal class RoleProwerService : IRoleProwerService
    {
        public void Set ( long roleId, long[] prowerId )
        {
            new SetRolePrower
            {
                RoleId = roleId,
                ProwerId = prowerId
            }.Send();
        }
        public long[] GetProwerId ( long roleId )
        {
            return new GetRoleProwerId
            {
                RoleId = roleId
            }.Send();
        }
        public OperateProwerDto[] Gets ( long prowerId )
        {
            return new GetsOperatePrower
            {
                ProwerId = prowerId
            }.Send();
        }
    }
}
