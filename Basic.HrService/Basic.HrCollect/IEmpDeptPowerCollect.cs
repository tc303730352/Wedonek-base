using Basic.HrModel.DeptPower;

namespace Basic.HrCollect
{
    public interface IEmpDeptPowerCollect
    {
        void Clear ( long empId );

        long[] GetDeptId ( long empId, long companyId );

        Dictionary<long, int> GetPowerNum ( long[] empId, long companyId );

        DeptPowerDto[] GetDepts ( long[] empId, long companyId );

        bool SetDeptId ( long empId, long companyId, KeyValuePair<long, long>[] depts );

        EmpDeptPower[] GetDeptPower ( long[] empId, long[] deptId );

        void Add ( DeptPowerAdd add );
    }
}