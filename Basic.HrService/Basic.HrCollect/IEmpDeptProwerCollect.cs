using Basic.HrModel.DeptPrower;

namespace Basic.HrCollect
{
    public interface IEmpDeptProwerCollect
    {
        void Clear (long empId);
        long[] GetDeptId (long empId, long companyId);

        DeptProwerDto[] GetDepts (long[] empId, long companyId);
        bool SetDeptId (long empId, long companyId, KeyValuePair<long, long>[] depts);
        EmpDeptPrower[] GetDeptPrower (long[] empId, long[] deptId);
        void Add (DeptProwerAdd add);
    }
}