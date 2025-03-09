using Basic.HrModel.DB;
using Basic.HrModel.OperatePower;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class RoleOperatePowerDAL : BasicDAL<DBRoleOperatePower, long>, IRoleOperatePowerDAL
    {
        public RoleOperatePowerDAL ( IRepository<DBRoleOperatePower> basicDAL ) : base(basicDAL)
        {
        }

        public long Add ( DBRoleOperatePower add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        private void _Add ( OperateItem[] powers, long powerId, long roleId )
        {
            this._BasicDAL.Insert(powers.ConvertAll(a => new DBRoleOperatePower
            {
                Id = IdentityHelper.CreateId(),
                RoleId = roleId,
                OperateId = a.Id,
                OperateVal = a.OperateVal,
                PowerId = powerId
            }));
        }
        private void _Set ( long roleId, long powerId, OperateItem[] powers, long[] ids )
        {
            ISqlQueue<DBRoleOperatePower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(powers.ConvertAll(a => new DBRoleOperatePower
            {
                Id = IdentityHelper.CreateId(),
                RoleId = roleId,
                OperateVal = a.OperateVal,
                PowerId = powerId,
                OperateId = a.Id
            }));
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.operate.power.set.fail");
            }
        }
        public void Clear ( long roleId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if ( ids.IsNull() )
            {
                return;
            }
            if ( !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.role.operate.power.delete.fail");
            }
        }
        public void Set ( long roleId, long powerId, OperateItem[] powers )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId && a.PowerId == powerId, a => a.Id);
            if ( ids.IsNull() && powers.IsNull() )
            {
                return;
            }
            else if ( !ids.IsNull() && powers.IsNull() )
            {
                if ( !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
                {
                    throw new ErrorException("hr.role.operate.power.delete.fail");
                }
            }
            else if ( ids.IsNull() )
            {
                this._Add(powers, powerId, roleId);
            }
            else
            {
                this._Set(roleId, powerId, powers, ids);
            }
        }
    }
}
