using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.DeptPower;

namespace Basic.HrGatewaryModular.Services
{
    internal class DeptPowerService : IDeptPowerService
    {
        public long[] GetDeptPower ( long empId, long companyId )
        {
            return new GetDeptPower
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }

        public void SetDeptPower ( long empId, long companyId, long[] deptId )
        {
            new SetDeptPower
            {
                EmpId = empId,
                CompanyId = companyId,
                DeptId = deptId,
            }.Send();
        }

    }
}
