using Basic.HrRemoteModel.EmpRole.Model;

namespace Basic.HrCollect
{
    public interface IEmpRoleCollect
    {
        long[] GetEmpId (long roleId);
        Dictionary<long, int> GetEmpNum (long[] longs);
        long[] GetRoleId (long empId);
        EmpRole[] GetRoles (long empId);
        Dictionary<long, EmpRole[]> GetRoles (long[] empId);
        bool SetRole (long empId, long[] roleId);

        void Clear (long empId);
    }
}