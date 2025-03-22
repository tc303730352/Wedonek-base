using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Role;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class RoleService : IRoleService
    {
        public long AddRole ( RoleSet datum )
        {
            return new AddRole
            {
                Datum = datum,
            }.Send();
        }
        public RoleSelectItem[] GetRoleSelect ( long companyId )
        {
            return new GetRoleSelect
            {
                CompanyId = companyId,
            }.Send();
        }
        public void DeleteRole ( long id )
        {
            new DeleteRole
            {
                Id = id,
            }.Send();
        }


        public RoleDetailed GetRoleDetailed ( long id )
        {
            return new GetRoleDetailed
            {
                Id = id,
            }.Send();
        }



        public bool SetRole ( long id, RoleSet datum )
        {
            return new SetRole
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public PagingResult<RoleDatum> Query ( PagingParam<RoleGetParam> param )
        {
            return new QueryRole
            {
                Size = param.Size,
                SortName = param.SortName,
                Index = param.Index,
                IsDesc = param.IsDesc,
                NextId = param.NextId,
                Param = param.Query
            }.Send();
        }

        public void SetIsEnable ( long id, bool isEnable )
        {
            new SetRoleIsEnable
            {
                Id = id,
                IsEnable = isEnable,
            }.Send();
        }

        public void SetIsDefRole ( long id )
        {
            new SetIsDefRole
            {
                Id = id
            }.Send();
        }

        public void SetIsAdmin ( long id, bool isAdmin )
        {
            new SetRoleIsAdmin
            {
                Id = id,
                IsAdmin = isAdmin,
            }.Send();
        }
    }
}
