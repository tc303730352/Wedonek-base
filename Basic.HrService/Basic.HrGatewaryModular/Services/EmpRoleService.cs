using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.EmpRole;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpRoleService : IEmpRoleService
    {
        public long[] GetEmpRole ( long empId, long companyId )
        {
            return new GetEmpRole
            {
                EmpId = empId,
                CompanyId = companyId,
            }.Send();
        }

        public void SetEmpRole ( long empId, long companyId, long[] roleId )
        {
            new SetEmpRole
            {
                EmpId = empId,
                CompanyId = companyId,
                RoleId = roleId
            }.Send();
        }

    }
}
