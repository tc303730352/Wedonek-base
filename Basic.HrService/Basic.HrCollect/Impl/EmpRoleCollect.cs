using Basic.HrDAL;
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

        public Dictionary<long, int> GetEmpNum ( long[] roleId )
        {
            return this._EmpRole.GetEmpNum(roleId);
        }

        public long[] GetRoleId ( long empId )
        {
            return this._EmpRole.GetRoleId(empId);
        }
        public Dictionary<long, EmpRole[]> GetRoles ( long[] empId )
        {
            return this._EmpRole.GetRoles(empId);
        }
        public EmpRole[] GetRoles ( long empId )
        {
            return this._EmpRole.GetRoles(empId);
        }
        public bool SetRole ( long empId, long[] roleId )
        {
            long[] rids = this.GetRoleId(empId);
            if ( rids.Length == roleId.Length && rids.IsEqual(roleId) )
            {
                return false;
            }
            else if ( rids.IsNull() )
            {
                this._EmpRole.Add(empId, roleId);
            }
            else if ( roleId.IsNull() )
            {
                this._EmpRole.ClearByEmpId(empId);
            }
            else
            {
                this._EmpRole.Set(empId, roleId);
            }
            return true;
        }
    }
}
