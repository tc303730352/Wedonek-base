using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.EmpRole;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpRoleService : IEmpRoleService
    {
        public long[] GetEmpRole (long empId)
        {
            return new GetEmpRole
            {
                EmpId = empId,
            }.Send();
        }

        public void SetEmpRole (long empId, long[] roleId)
        {
            new SetEmpRole
            {
                EmpId = empId,
                RoleId = roleId
            }.Send();
        }

    }
}
