using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class RoleCollect : IRoleCollect
    {
        private readonly IRoleDAL _RoleDAL;
        private readonly IRolePowerDAL _RolePower;
        public RoleCollect ( IRoleDAL roleDAL, IRolePowerDAL rolePower )
        {
            this._RolePower = rolePower;
            this._RoleDAL = roleDAL;
        }
        public RoleBase[] GetBases ( long[] ids )
        {
            return this._RoleDAL.Gets<RoleBase>(ids);
        }
        public void CheckIsEnable ( long[] ids )
        {
            long[] existId = this._RoleDAL.Gets(a => ids.Contains(a.Id) && a.IsEnable, a => a.Id);
            if ( existId.Length != ids.Length )
            {
                throw new ErrorException("hr.role.no.enable");
            }
        }
        public long Add ( RoleSetDatum add, RolePower[] powers )
        {
            if ( this._RoleDAL.IsExists(a => a.RoleName == add.RoleName) )
            {
                throw new ErrorException("hr.role.name.repeat");
            }
            DBRole role = add.ConvertMap<RoleSetDatum, DBRole>();
            this._RoleDAL.Add(role, powers);
            return role.Id;
        }
        public long GetDefRoleId ()
        {
            return this._RoleDAL.GetDefRole();
        }
        public Result[] Query<Result> ( RoleGetParam param, IBasicPage paging, out int count ) where Result : class, new()
        {
            paging.InitOrderBy("Id", true);
            return this._RoleDAL.Query<Result>(param, paging, out count);
        }
        public RoleDto GetRole ( long roleId )
        {
            DBRole role = this._RoleDAL.Get(roleId);
            RoleDto dto = role.ConvertMap<DBRole, RoleDto>();
            dto.PowerId = this._RolePower.GetPowerId(roleId);
            return dto;
        }
        public bool Set ( DBRole role, RoleSetDatum set, RolePower[] powers )
        {
            if ( role.IsEnable )
            {
                throw new ErrorException("hr.role.not.can.update");
            }
            else if ( role.IsDefRole )
            {
                throw new ErrorException("hr.def.role.not.can.update");
            }
            else if ( set.RoleName != role.RoleName && this._RoleDAL.IsExists(a => a.RoleName == set.RoleName) )
            {
                throw new ErrorException("hr.role.name.repeat");
            }
            long[] ids = this._RolePower.Gets(a => a.RoleId == role.Id, a => a.Id);
            if ( ids.IsNull() && powers.IsNull() )
            {
                return this._RoleDAL.Update(role, set);
            }
            this._RoleDAL.Set(role, set, ids, powers);
            return true;
        }
        public void Delete ( DBRole role )
        {
            if ( role.IsEnable )
            {
                throw new ErrorException("hr.role.not.can.delete");
            }
            else if ( role.IsDefRole )
            {
                throw new ErrorException("hr.def.role.not.can.delete");
            }
            long[] ids = this._RolePower.Gets(a => a.RoleId == role.Id, a => a.Id);
            this._RoleDAL.Delete(role, ids);
        }

        public DBRole Get ( long roleId )
        {
            return this._RoleDAL.Get(roleId);
        }

        public bool SetIsEnable ( DBRole role, bool enable )
        {
            if ( role.IsDefRole )
            {
                throw new ErrorException("hr.def.role.not.can.delete");
            }
            else if ( role.IsEnable == enable )
            {
                return false;
            }
            else if ( enable && !role.IsAdmin && !this._RolePower.IsExists(c => c.RoleId == role.Id) )
            {
                throw new ErrorException("hr.role.power.null");
            }
            this._RoleDAL.SetIsEnable(role, enable);
            return true;
        }
        public bool SetIsAdmin ( DBRole role, bool isAdmin )
        {
            if ( role.IsAdmin == isAdmin )
            {
                return false;
            }
            bool isEnable = role.IsEnable;
            if ( isEnable )
            {
                isEnable = this._RolePower.IsExists(c => c.RoleId == role.Id);
            }
            this._RoleDAL.SetIsAdmin(role, isAdmin, isEnable);
            return true;
        }
        public bool CheckIsAdmin ( long[] roleId )
        {
            return this._RoleDAL.CheckIsAdmin(roleId);
        }

        public RoleSelectItem[] GetSelect ()
        {
            return this._RoleDAL.Gets<RoleSelectItem>(a => a.IsEnable);
        }

        public void SetIsDef ( DBRole role, long defId )
        {
            this._RoleDAL.SetIsDef(role, defId);
        }
    }
}
