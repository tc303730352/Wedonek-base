using Basic.HrModel.DB;
using Basic.HrModel.RolePrower;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class RoleProwerDAL : BasicDAL<DBRolePrower, long>, IRoleProwerDAL
    {
        public RoleProwerDAL (IRepository<DBRolePrower> basicDAL) : base(basicDAL)
        {
        }
        public void Clear (long roleId)
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if (!ids.IsNull() && !this._BasicDAL.Delete(a => ids.Contains(a.Id)))
            {
                throw new ErrorException("hr.role.prower.clear.fail");
            }
        }
        public long[] GetProwerId (long[] roleId, ProwerType prowerType)
        {
            return this._BasicDAL.Gets(a => roleId.Contains(a.RoleId) && a.ProwerType == prowerType, a => a.ProwerId).Distinct();
        }
        public long[] GetProwerId (long[] roleId, long subSysId, ProwerType prowerType)
        {
            return this._BasicDAL.Gets(a => roleId.Contains(a.RoleId) && a.SubSystemId == subSysId && a.ProwerType == prowerType, a => a.ProwerId).Distinct();
        }
        public long[] GetProwerId (long roleId)
        {
            return this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.ProwerId);
        }

        public long[] GetSubSysId (long[] roleId)
        {
            return this._BasicDAL.GroupBy(a => roleId.Contains(a.RoleId) && a.ProwerType == ProwerType.menu, a => a.SubSystemId, a => a.SubSystemId);
        }

        public void Set (long roleId, RolePrower[] prowers)
        {
            long[] ids = this._BasicDAL.Gets(a => a.RoleId == roleId, a => a.Id);
            if (ids.IsNull() && prowers.IsNull())
            {
                return;
            }
            else if (ids.IsNull())
            {
                this._BasicDAL.Insert(prowers.ConvertAll(a => new DBRolePrower
                {
                    Id = IdentityHelper.CreateId(),
                    SubSystemId = a.SubSystemId,
                    ProwerId = a.ProwerId,
                    ProwerType = a.ProwerType,
                    RoleId = roleId
                }));
                return;
            }
            ISqlQueue<DBRolePrower> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => ids.Contains(a.Id));
            queue.Insert(prowers.ConvertAll(a => new DBRolePrower
            {
                Id = IdentityHelper.CreateId(),
                SubSystemId = a.SubSystemId,
                ProwerType = a.ProwerType,
                ProwerId = a.ProwerId,
                RoleId = roleId
            }));
            if (queue.Submit() <= 0)
            {
                throw new ErrorException("hr.role.prower.set.fail");
            }
        }
    }
}
