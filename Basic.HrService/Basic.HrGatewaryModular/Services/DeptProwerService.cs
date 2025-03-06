using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.DeptPrower;

namespace Basic.HrGatewaryModular.Services
{
    internal class DeptProwerService : IDeptProwerService
    {
        public long[] GetDeptPrower (long empId, long companyId)
        {
            return new GetDeptPrower
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }

        public void SetDeptPrower (long empId, long companyId, long[] deptId)
        {
            new SetDeptPrower
            {
                EmpId = empId,
                CompanyId = companyId,
                DeptId = deptId,
            }.Send();
        }

    }
}
