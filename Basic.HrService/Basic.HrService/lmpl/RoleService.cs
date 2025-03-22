using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleCollect _Role;
        private readonly IPowerCollect _Power;
        private readonly IEmpRoleCollect _EmpRole;
        public RoleService ( IRoleCollect role,
            IPowerCollect power,
            IEmpRoleCollect empRole )
        {
            this._Role = role;
            this._Power = power;
            this._EmpRole = empRole;
        }
        public void SetIsEnable ( long id, bool enable )
        {
            DBRole role = this._Role.Get(id);
            if ( this._Role.SetIsEnable(role, enable) )
            {
                new RoleEvent(role).AsyncPublic("SetIsEnable");
            }
        }
        public long Add ( RoleSet add )
        {
            RolePower[] powers = null;
            if ( !add.PowerId.IsNull() )
            {
                PowerBasic[] list = this._Power.GetFulls(add.PowerId);
                if ( list.IsNull() )
                {
                    throw new ErrorException("hr.power.not.find");
                }
                powers = list.ConvertAll(c => new RolePower
                {
                    PowerId = c.Id,
                    SubSystemId = c.SubSystemId,
                    PowerType = c.PowerType
                });
            }
            RoleSetDatum datum = add.ConvertMap<RoleSet, RoleSetDatum>();
            return this._Role.Add(datum, powers);
        }
        public bool Set ( long roleId, RoleSet set )
        {
            DBRole role = this._Role.Get(roleId);
            RolePower[] powers = null;
            if ( !set.PowerId.IsNull() )
            {
                PowerBasic[] list = this._Power.GetFulls(set.PowerId);
                if ( list.IsNull() )
                {
                    throw new ErrorException("hr.power.not.find");
                }
                powers = list.ConvertAll(c => new RolePower
                {
                    PowerId = c.Id,
                    SubSystemId = c.SubSystemId,
                    PowerType = c.PowerType
                });
            }
            RoleSetDatum datum = set.ConvertMap<RoleSet, RoleSetDatum>();
            if ( this._Role.Set(role, datum, powers) )
            {
                new RoleEvent(role).AsyncPublic("Update");
                return true;
            }
            return false;
        }
        public RoleDetailed GetDetailed ( long id )
        {
            RoleDto role = this._Role.GetRole(id);
            RoleDetailed dto = role.ConvertMap<RoleDto, RoleDetailed>();
            return dto;
        }
        public void Delete ( long id )
        {
            DBRole role = this._Role.Get(id);
            this._Role.Delete(role);
            new RoleEvent(role).AsyncPublic("Delete");
        }

        public RoleDatum Get ( long id )
        {
            DBRole role = this._Role.Get(id);
            return role.ConvertMap<DBRole, RoleDatum>();
        }
        public RoleSelectItem[] GetSelect ( long companyId )
        {
            return this._Role.GetSelect(companyId);
        }
        public PagingResult<RoleDatum> Query ( RoleGetParam param, IBasicPage paging )
        {
            DBRole[] roles = this._Role.Query<DBRole>(param, paging, out int count);
            if ( roles.IsNull() )
            {
                return new PagingResult<RoleDatum>();
            }
            RoleDatum[] dtos = roles.ConvertMap<DBRole, RoleDatum>();
            Dictionary<long, int> dic = this._EmpRole.GetEmpNum(param.CompanyId, dtos.ConvertAll(c => c.Id));
            dtos.ForEach(c =>
            {
                c.EmpNum = dic.GetValueOrDefault(c.Id);
            });
            return new PagingResult<RoleDatum>(dtos, count);
        }
        public void SetIsAdmin ( long id, bool isAdmin )
        {
            DBRole role = this._Role.Get(id);
            if ( this._Role.SetIsAdmin(role, isAdmin) )
            {
                new RoleEvent(role).AsyncPublic("SetIsAdmin");
            }
        }
        public void SetDefRole ( long id )
        {
            DBRole role = this._Role.Get(id);
            if ( role.IsDefRole )
            {
                return;
            }
            else if ( !role.IsEnable )
            {
                throw new ErrorException("hr.def.role.must.enable");
            }
            long defId = this._Role.GetDefRoleId(role.CompanyId);
            this._Role.SetIsDef(role, defId);
        }
    }
}
