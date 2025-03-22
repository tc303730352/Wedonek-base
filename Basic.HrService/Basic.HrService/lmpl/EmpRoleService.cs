using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class EmpRoleService : IEmpRoleService
    {
        private readonly IEmpRoleCollect _EmpRole;
        private readonly IRoleCollect _Role;

        public EmpRoleService ( IEmpRoleCollect empRole, IRoleCollect role )
        {
            this._EmpRole = empRole;
            this._Role = role;
        }

        public void SetRole ( long empId, long companyId, long[] roleId )
        {
            this._Role.CheckIsEnable(roleId);
            if ( this._EmpRole.SetRole(empId, companyId, roleId) )
            {
                new UserChangeEvent(empId).AsyncSend("RoleChange");
            }
        }
        public long[] GetRoleId ( long empId, long companyId )
        {
            return this._EmpRole.GetRoleId(companyId, empId);
        }
    }
}
