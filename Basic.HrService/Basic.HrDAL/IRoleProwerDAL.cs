using Basic.HrModel.DB;
using Basic.HrModel.RolePrower;
using Basic.HrRemoteModel;

namespace Basic.HrDAL
{
    public interface IRoleProwerDAL : IBasicDAL<DBRolePrower, long>
    {
        void Clear (long roleId);
        long[] GetProwerId (long[] roleId, long subSysId, ProwerType prowerType);
        long[] GetProwerId (long[] roleId, ProwerType prowerType);
        long[] GetProwerId (long roleId);
        long[] GetSubSysId (long[] roleId);
        void Set (long roleId, RolePrower[] prowers);
    }
}