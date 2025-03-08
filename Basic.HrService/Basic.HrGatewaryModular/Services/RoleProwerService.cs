using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.RolePrower;
using Basic.HrRemoteModel.RolePrower;

namespace Basic.HrGatewaryModular.Services
{
    internal class RoleProwerService : IRoleProwerService
    {
        public void Set ( RoleProwerSet param )
        {
            new SetRolePrower
            {
                RoleId = param.RoleId,
                ProwerId = param.ProwerId,
                OperateId = param.OperateId
            }.Send();
        }
    }
}
