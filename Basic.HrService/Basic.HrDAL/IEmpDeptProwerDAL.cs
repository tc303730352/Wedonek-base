using Basic.HrModel.DB;
using Basic.HrModel.DeptPrower;

namespace Basic.HrDAL
{
    public interface IEmpDeptProwerDAL : IBasicDAL<DBEmpDeptPrower, long>
    {
        DeptProwerDto[] GetDepts (long[] empId, long companyId);
        void Set (long[] ids, DBEmpDeptPrower[] adds);
        void Add (DeptProwerAdd add);
    }
}