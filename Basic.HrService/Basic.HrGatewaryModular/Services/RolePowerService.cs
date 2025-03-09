using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.RolePower;
using Basic.HrRemoteModel.RolePower;

namespace Basic.HrGatewaryModular.Services
{
    internal class RolePowerService : IRolePowerService
    {
        public void Set ( RolePowerSet param )
        {
            new SetRolePower
            {
                RoleId = param.RoleId,
                PowerId = param.PowerId,
                OperateId = param.OperateId
            }.Send();
        }
    }
}
