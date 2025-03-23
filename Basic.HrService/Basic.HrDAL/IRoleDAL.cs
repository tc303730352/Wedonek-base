using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IRoleDAL : IBasicDAL<DBRole, long>
    {
        void Delete ( DBRole role, long[] rolePowerId );
        void Set ( DBRole role, RoleSetDatum set, long[] rolePowerId, RolePower[] power );
        void Add ( DBRole add, RolePower[] power );
        long GetDefRole ( long companyId );
        void SetIsEnable ( DBRole role, bool enable );
        bool CheckIsAdmin ( long companyId, long[] roleId );
        Result[] Query<Result> ( RoleGetParam param, IBasicPage paging, out int count ) where Result : class, new();
        void SetIsDef ( DBRole role, long defId );
        void SetIsAdmin ( DBRole role, bool isAdmin, bool isEnable );
        void Add ( DBRole role );
    }
}