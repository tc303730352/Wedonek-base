using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IRoleService
    {
        long Add ( long companyId, RoleSet add );
        void Delete ( long id );
        RoleDetailed GetDetailed ( long id );
        void SetIsAdmin ( long id, bool isAdmin );
        RoleDatum Get ( long id );

        bool Set ( long roleId, RoleSet set );
        void SetIsEnable ( long id, bool enable );
        RoleSelectItem[] GetSelect ( long companyId );
        PagingResult<RoleDatum> Query ( RoleGetParam param, IBasicPage paging );
        void SetDefRole ( long id );
    }
}