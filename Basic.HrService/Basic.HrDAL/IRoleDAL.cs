using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePrower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IRoleDAL : IBasicDAL<DBRole, long>
    {
        void Delete (DBRole role, long[] roleProwerId);
        void Set (DBRole role, RoleSetDatum set, long[] roleProwerId, RolePrower[] prower);
        void Add (DBRole add, RolePrower[] prower);
        long GetDefRole ();
        void SetIsEnable (DBRole role, bool enable);
        bool CheckIsAdmin (long[] roleId);
        Result[] Query<Result> (RoleGetParam param, IBasicPage paging, out int count) where Result : class, new();
        void SetIsDef (DBRole role, long defId);
        void SetIsAdmin (DBRole role, bool isAdmin, bool isEnable);
    }
}