using Basic.HrModel.DB;
using Basic.HrRemoteModel.EmpRole.Model;

namespace Basic.HrDAL
{
    public interface IEmpRoleDAL : IBasicDAL<DBEmpRole, long>
    {
        Dictionary<long, EmpRole[]> GetRoles (long[] empId);
        EmpRole[] GetRoles (long empId);
        void Clear (long roleId);
        void ClearByEmpId (long empId);
        void Add (long empId, long[] roleId);
        long[] GetRoleId (long empId);
        void Set (long empId, long[] roleId);
        Dictionary<long, int> GetEmpNum (long[] roleId);
    }
}