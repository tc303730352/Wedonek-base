using Basic.HrModel.DB;
using Basic.HrModel.RolePower;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class RolePowerDAL : BasicDAL<DBRolePower, long>, IRolePowerDAL
    {
        public RolePowerDAL ( IRepository<DBRolePower> basicDAL ) : base(basicDAL)
        {
        }
        public void Clear ( long roleId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if ( !ids.IsNull() && !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.role.power.clear.fail");
            }
        }
        public long[] GetPowerId ( long[] roleId )
        {
            return this._BasicDAL.Gets(a => roleId.Contains(a.RoleId), a => a.PowerId).Distinct();
        }
        public long[] GetPowerId ( long roleId )
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.PowerId);
        }

        public long[] GetSubSysId ( long[] roleId )
        {
            return this._BasicDAL.GroupBy(a => roleId.Contains(a.RoleId), a => a.SubSystemId, a => a.SubSystemId);
        }

        public void Set ( long roleId, RolePower[] powers )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if ( ids.IsNull() && powers.IsNull() )
            {
                return;
            }
            else if ( ids.IsNull() )
            {
                this._BasicDAL.Insert(powers.ConvertAll(a => new DBRolePower
                {
                    Id = IdentityHelper.CreateId(),
                    SubSystemId = a.SubSystemId,
                    PowerId = a.PowerId,
                    RoleId = roleId
                }));
                return;
            }
            ISqlQueue<DBRolePower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(powers.ConvertAll(a => new DBRolePower
            {
                Id = IdentityHelper.CreateId(),
                SubSystemId = a.SubSystemId,
                PowerId = a.PowerId,
                RoleId = roleId
            }));
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.power.set.fail");
            }
        }
    }
}
