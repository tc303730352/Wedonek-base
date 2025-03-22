using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class RoleDAL : BasicDAL<DBRole, long>, IRoleDAL
    {
        public RoleDAL ( IRepository<DBRole> basicDAL ) : base(basicDAL)
        {
        }
        public void Delete ( DBRole role, long[] rolePowerId )
        {
            if ( rolePowerId.IsNull() )
            {
                base.Delete(role.Id);
                return;
            }
            ISqlQueue<DBRole> queue = this._BasicDAL.BeginQueue();
            queue.DeleteBy<DBRolePower>(a => rolePowerId.Contains(a.Id));
            queue.Delete(a => a.Id == role.Id);
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.delete.fail");
            }
        }
        public void Set ( DBRole role, RoleSetDatum set, long[] rolePowerId, RolePower[] power )
        {
            ISqlQueue<DBRole> queue = this._BasicDAL.BeginQueue();
            _ = queue.Update(role, set);
            if ( !rolePowerId.IsNull() )
            {
                queue.DeleteBy<DBRolePower>(a => rolePowerId.Contains(a.Id));
            }
            if ( !power.IsNull() )
            {
                queue.InsertBy(power.ConvertAll(a => new DBRolePower
                {
                    Id = IdentityHelper.CreateId(),
                    SubSystemId = a.SubSystemId,
                    PowerId = a.PowerId,
                    PowerType = a.PowerType,
                    RoleId = role.Id
                }));
            }
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.role.set.fail");
            }
        }
        public void Add ( DBRole add, RolePower[] power )
        {
            add.Id = IdentityHelper.CreateId();
            add.AddTime = DateTime.Now;
            if ( power.IsNull() )
            {
                this._BasicDAL.Insert(add);
            }
            else
            {
                ISqlQueue<DBRole> queue = this._BasicDAL.BeginQueue();
                queue.Insert(add);
                queue.InsertBy(power.ConvertAll(a => new DBRolePower
                {
                    Id = IdentityHelper.CreateId(),
                    SubSystemId = a.SubSystemId,
                    PowerId = a.PowerId,
                    PowerType = a.PowerType,
                    RoleId = add.Id
                }));
                if ( queue.Submit() <= 0 )
                {
                    throw new ErrorException("hr.role.add.fail");
                }
            }
        }
        public void SetIsEnable ( DBRole role, bool enable )
        {
            if ( !this._BasicDAL.Update(a => a.IsEnable == enable, a => a.Id == role.Id) )
            {
                throw new ErrorException("hr.role.enable.set.fail");
            }
        }
        public void SetIsAdmin ( DBRole role, bool isAdmin, bool enable )
        {
            if ( !this._BasicDAL.Update(a => new DBRole
            {
                IsEnable = enable,
                IsAdmin = isAdmin,
            }, a => a.Id == role.Id) )
            {
                throw new ErrorException("hr.role.admin.set.fail");
            }
        }

        public Result[] Query<Result> ( RoleGetParam query, IBasicPage paging, out int count ) where Result : class, new()
        {
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }

        public long GetDefRole ( long companyId )
        {
            return this._BasicDAL.Get(a => a.CompanyId == companyId && a.IsEnable && a.IsDefRole, a => a.Id);
        }
        public void SetIsDef ( DBRole role, long defId )
        {
            ISqlQueue<DBRole> queue = this._BasicDAL.BeginQueue();
            queue.UpdateOneColumn(a => a.IsDefRole == true, a => a.Id == role.Id);
            queue.UpdateOneColumn(a => a.IsDefRole == false, a => a.Id == defId);
            _ = queue.Submit();
        }
        public bool CheckIsAdmin ( long companyId, long[] roleId )
        {
            return this._BasicDAL.IsExist(a => a.CompanyId == companyId && roleId.Contains(a.Id) && a.IsAdmin);
        }
    }
}
