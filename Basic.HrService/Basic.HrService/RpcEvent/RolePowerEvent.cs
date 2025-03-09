using Basic.HrRemoteModel.RolePower;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class RolePowerEvent : IRpcApiService
    {
        private readonly IRoleOperatePowerService _Service;

        public RolePowerEvent ( IRoleOperatePowerService service )
        {
            this._Service = service;
        }

        public void SetRolePower ( SetRolePower obj )
        {
            this._Service.Set(obj.RoleId, obj.PowerId, obj.OperateId);
        }
        public long[] GetRoleOperateId ( GetRoleOperateId obj )
        {
            return this._Service.GetOperateId(obj.RoleId, obj.PowerId);
        }
    }
}
