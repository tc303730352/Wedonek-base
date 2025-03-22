using Basic.HrRemoteModel.EmpRole;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class EmpRoleEvent : IRpcApiService
    {
        private readonly IEmpRoleService _Service;

        public EmpRoleEvent ( IEmpRoleService service )
        {
            this._Service = service;
        }

        public long[] GetEmpRole ( GetEmpRole obj )
        {
            return this._Service.GetRoleId(obj.EmpId, obj.CompanyId);
        }
        public void SetEmpRole ( SetEmpRole obj )
        {
            this._Service.SetRole(obj.EmpId, obj.CompanyId, obj.RoleId);
        }
    }
}
