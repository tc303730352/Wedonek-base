using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrRemoteModel.EmpRole.Model;

namespace Basic.HrDAL
{
    public interface IEmpRoleDAL : IBasicDAL<DBEmpRole, long>
    {
        long[] GetRoleId ( long companyId, long empId );
        Dictionary<long, EmpRole[]> GetRoles ( long companyId, long[] empId );
        EmpRoleBase[] GetEmpRoles ( long empId );
        EmpRole[] GetRoles ( long companyId, long empId );
        void Clear ( long roleId );
        void ClearByEmpId ( long companyId, long empId );
        void Add ( long empId, long companyId, long[] roleId );
        void Set ( long empId, long companyId, long[] roleId );
        Dictionary<long, int> GetEmpNum ( long companyId, long[] roleId );
        void ClearByEmpId ( long empId );
    }
}