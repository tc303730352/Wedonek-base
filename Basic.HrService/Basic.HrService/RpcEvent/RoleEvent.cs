using Basic.HrRemoteModel.Role;
using Basic.HrRemoteModel.Role.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class RoleEvent : IRpcApiService
    {
        private readonly IRoleService _Service;

        public RoleEvent ( IRoleService service )
        {
            this._Service = service;
        }
        public RoleSelectItem[] GetRoleSelect ( GetRoleSelect obj )
        {
            return this._Service.GetSelect(obj.CompanyId);
        }
        public long AddRole ( AddRole add )
        {
            return this._Service.Add(add.CompanyId, add.Datum);
        }
        public bool SetRole ( SetRole obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
        public void SetRoleIsAdmin ( SetRoleIsAdmin obj )
        {
            this._Service.SetIsAdmin(obj.Id, obj.IsAdmin);
        }
        public void DeleteRole ( DeleteRole obj )
        {
            this._Service.Delete(obj.Id);
        }
        public RoleDetailed GetRoleDetailed ( GetRoleDetailed obj )
        {
            return this._Service.GetDetailed(obj.Id);
        }
        public PagingResult<RoleDatum> QueryRole ( QueryRole obj )
        {
            return this._Service.Query(obj.Param, obj.ToBasicPage());
        }
        public void SetIsDefRole ( SetIsDefRole obj )
        {
            this._Service.SetDefRole(obj.Id);
        }
        public void SetRoleIsEnable ( SetRoleIsEnable obj )
        {
            this._Service.SetIsEnable(obj.Id, obj.IsEnable);
        }
    }
}
