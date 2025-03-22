using Basic.HrDAL;
using Basic.HrModel.Role;
using Basic.HrRemoteModel.EmpRole.Model;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class EmpRoleCollect : IEmpRoleCollect
    {
        private readonly IEmpRoleDAL _EmpRole;

        public EmpRoleCollect ( IEmpRoleDAL empRole )
        {
            this._EmpRole = empRole;
        }
        public void Clear ( long empId )
        {
            this._EmpRole.ClearByEmpId(empId);
        }
        public void Clear ( long companyId, long empId )
        {
            this._EmpRole.ClearByEmpId(companyId, empId);
        }
        public void ClearByRoleId ( long roleId )
        {
            long[] ids = this._EmpRole.Gets(a => a.RoleId == roleId, a => a.Id);
            if ( ids.IsNull() )
            {
                return;
            }
            this._EmpRole.Delete(ids);
        }
        public long[] GetEmpId ( long roleId )
        {
            return this._EmpRole.Gets(a => a.RoleId == roleId, a => a.EmpId);
        }

        public Dictionary<long, int> GetEmpNum ( long companyId, long[] roleId )
        {
            return this._EmpRole.GetEmpNum(companyId, roleId);
        }

        public EmpRoleBase[] GetEmpRoles ( long empId )
        {
            return this._EmpRole.GetEmpRoles(empId);
        }

        public long[] GetRoleId ( long companyId, long empId )
        {
            return this._EmpRole.GetRoleId(companyId, empId);
        }

        public Dictionary<long, EmpRole[]> GetRoles ( long companyId, long[] empId )
        {
            return this._EmpRole.GetRoles(companyId, empId);
        }
        public EmpRole[] GetRoles ( long companyId, long empId )
        {
            return this._EmpRole.GetRoles(companyId, empId);
        }

        public bool SetRole ( long companyId, long empId, long[] roleId )
        {
            long[] rids = this._EmpRole.GetRoleId(companyId, empId);
            if ( rids.Length == roleId.Length && rids.IsEqual(roleId) )
            {
                return false;
            }
            else if ( rids.IsNull() )
            {
                this._EmpRole.Add(empId, companyId, roleId);
            }
            else if ( roleId.IsNull() )
            {
                this._EmpRole.ClearByEmpId(empId);
            }
            else
            {
                this._EmpRole.Set(empId, companyId, roleId);
            }
            return true;
        }
    }
}
