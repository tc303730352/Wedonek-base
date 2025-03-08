using Basic.HrModel.DB;
using Basic.HrModel.OperatePrower;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class RoleOperateProwerDAL : BasicDAL<DBRoleOperatePrower, long>, IRoleOperateProwerDAL
    {
        public RoleOperateProwerDAL ( IRepository<DBRoleOperatePrower> basicDAL ) : base(basicDAL)
        {
        }

        public long Add ( DBRoleOperatePrower add )
        {
            add.Id = IdentityHelper.CreateId();
            this._BasicDAL.Insert(add);
            return add.Id;
        }
        private void _Add ( OperateItem[] prowers, long prowerId, long roleId )
        {
            this._BasicDAL.Insert(prowers.ConvertAll(a => new DBRoleOperatePrower
            {
                Id = IdentityHelper.CreateId(),
                RoleId = roleId,
                OperateId = a.Id,
                OperateVal = a.OperateVal,
                ProwerId = prowerId
            }));
        }
        private void _Set ( long roleId, long prowerId, OperateItem[] prowers, long[] ids )
        {
            ISqlQueue<DBRoleOperatePrower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(prowers.ConvertAll(a => new DBRoleOperatePrower
            {
                Id = IdentityHelper.CreateId(),
                RoleId = roleId,
                OperateVal = a.OperateVal,
                ProwerId = prowerId,
                OperateId = a.Id
            }));
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.operate.prower.set.fail");
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
                throw new ErrorException("hr.role.operate.prower.delete.fail");
            }
        }
        public void Set ( long roleId, long prowerId, OperateItem[] prowers )
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId && a.ProwerId == prowerId, a => a.Id);
            if ( ids.IsNull() && prowers.IsNull() )
            {
                return;
            }
            else if ( !ids.IsNull() && prowers.IsNull() )
            {
                if ( !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
                {
                    throw new ErrorException("hr.role.operate.prower.delete.fail");
                }
            }
            else if ( ids.IsNull() )
            {
                this._Add(prowers, prowerId, roleId);
            }
            else
            {
                this._Set(roleId, prowerId, prowers, ids);
            }
        }
    }
}
