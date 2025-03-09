using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IRoleCollect
    {
        bool SetIsAdmin ( DBRole role, bool isAdmin );
        void CheckIsEnable ( long[] ids );

        RoleDto GetRole ( long roleId );

        long Add ( RoleSetDatum add, RolePower[] powers );

        void Delete ( DBRole role );

        DBRole Get ( long roleId );

        long GetDefRoleId ();

        RoleBase[] GetBases ( long[] ids );


        bool Set ( DBRole role, RoleSetDatum set, RolePower[] powers );

        bool SetIsEnable ( DBRole role, bool enable );

        bool CheckIsAdmin ( long[] roleId );
        RoleSelectItem[] GetSelect ();
        Result[] Query<Result> ( RoleGetParam param, IBasicPage paging, out int count ) where Result : class, new();
        void SetIsDef ( DBRole role, long defId );
    }
}