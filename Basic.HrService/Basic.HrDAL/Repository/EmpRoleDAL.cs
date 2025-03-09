using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.EmpRole.Model;
using SqlSugar;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class EmpRoleDAL : BasicDAL<DBEmpRole, long>, IEmpRoleDAL
    {
        public EmpRoleDAL ( IRepository<DBEmpRole> basicDAL ) : base(basicDAL)
        {
        }
        public Dictionary<long, int> GetEmpNum ( long[] roleId )
        {
            return this._BasicDAL.GroupBy(a => roleId.Contains(a.RoleId), a => a.RoleId, a => new
            {
                a.RoleId,
                num = SqlFunc.AggregateCount(a.RoleId)
            }).ToDictionary(a => a.RoleId, a => a.num);
        }
        public long[] GetRoleId ( long empId )
        {
            return this._BasicDAL.Join<DBRole, long>(( a, b ) => a.RoleId == b.Id && b.IsEnable && a.EmpId == empId, ( a, b ) => b.Id);
        }
        public Dictionary<long, EmpRole[]> GetRoles ( long[] empId )
        {
            EmpRoleDto[] list = this._BasicDAL.Join<DBRole, EmpRoleDto>(( a, b ) => a.RoleId == b.Id && empId.Contains(a.EmpId) && b.IsEnable, ( a, b ) => new EmpRoleDto
            {
                RoleId = a.RoleId,
                EmpId = a.EmpId,
                RoleName = b.RoleName,
                IsAdmin = b.IsAdmin
            });
            return list.GroupBy(a => a.EmpId).ToDictionary(a => a.Key, a => a.Select(a => new EmpRole
            {
                RoleId = a.RoleId,
                RoleName = a.RoleName,
                IsAdmin = a.IsAdmin,
            }).ToArray());
        }
        public EmpRole[] GetRoles ( long empId )
        {
            return this._BasicDAL.Join<DBRole, EmpRole>(( a, b ) => a.RoleId == b.Id && b.IsEnable && a.EmpId == empId, ( a, b ) => new EmpRole
            {
                RoleId = a.RoleId,
                RoleName = b.RoleName,
                IsAdmin = b.IsAdmin,
            });
        }
        public void Clear ( long roleId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if ( !ids.IsNull() && !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.emp.role.clear.fail");
            }
        }
        public void ClearByEmpId ( long empId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.EmpId == empId, a => a.Id);
            if ( !ids.IsNull() && !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.emp.role.clear.fail");
            }
        }

        public void Add ( long empId, long[] roleId )
        {
            this._BasicDAL.Insert(roleId.ConvertAll(c => new DBEmpRole
            {
                EmpId = empId,
                Id = IdentityHelper.CreateId(),
                RoleId = c
            }));
        }
        public void Set ( long empId, long[] roleId )
        {
            ISqlQueue<DBEmpRole> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => a.EmpId == empId);
            queue.Insert(roleId.ConvertAll(a => new DBEmpRole
            {
                Id = IdentityHelper.CreateId(),
                EmpId = empId,
                RoleId = a
            }));
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.power.set.fail");
            }
        }
    }
}
