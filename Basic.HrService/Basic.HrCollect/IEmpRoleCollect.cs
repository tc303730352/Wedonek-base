using Basic.HrModel.Role;
using Basic.HrRemoteModel.EmpRole.Model;

namespace Basic.HrCollect
{
    public interface IEmpRoleCollect
    {
        EmpRole[] GetRoles ( long companyId, long empId );
        long[] GetEmpId ( long roleId );
        Dictionary<long, int> GetEmpNum ( long companyId, long[] roleId );
        long[] GetRoleId ( long companyId, long empId );
        EmpRoleBase[] GetEmpRoles ( long empId );
        Dictionary<long, EmpRole[]> GetRoles ( long companyId, long[] empId );
        bool SetRole ( long empId, long companyId, long[] roleId );
        void ClearByRoleId ( long roleId );
        void Clear ( long empId );
        void Clear ( long companyId, long empId );
    }
}