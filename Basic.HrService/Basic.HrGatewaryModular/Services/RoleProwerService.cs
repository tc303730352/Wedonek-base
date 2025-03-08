using Basic.HrGatewaryModular.Interface;
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
        public long[] GetOperateId ( long roleId )
        {
            return new GetRoleOperateId
            {
                RoleId = roleId
            }.Send();
        }
    }
}
