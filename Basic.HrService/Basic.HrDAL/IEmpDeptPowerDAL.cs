using Basic.HrModel.DB;
using Basic.HrModel.DeptPower;

namespace Basic.HrDAL
{
    public interface IEmpDeptPowerDAL : IBasicDAL<DBEmpDeptPower, long>
    {
        Dictionary<long, int> GetPowerNum ( long[] empId, long companyId );
        DeptPowerDto[] GetDepts ( long[] empId, long companyId );
        void Set ( long[] ids, DBEmpDeptPower[] adds );
        void Add ( DeptPowerAdd add );
    }
}