using Basic.HrRemoteModel.RolePrower;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class RoleProwerEvent : IRpcApiService
    {
        private readonly IRoleOperateProwerService _Service;

        public RoleProwerEvent ( IRoleOperateProwerService service )
        {
            this._Service = service;
        }

        public void SetRolePrower ( SetRolePrower obj )
        {
            this._Service.Set(obj.RoleId, obj.ProwerId);
        }
        public long[] GetRoleOperateId ( GetRoleOperateId obj )
        {
            return this._Service.GetOperateId(obj.RoleId, obj.ProwerId);
        }
    }
}
