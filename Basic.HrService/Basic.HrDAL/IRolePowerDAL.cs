using Basic.HrModel.DB;
using Basic.HrModel.RolePower;

namespace Basic.HrDAL
{
    public interface IRolePowerDAL : IBasicDAL<DBRolePower, long>
    {
        void Clear ( long roleId );
        long[] GetPowerId ( long[] roleId );
        long[] GetPowerId ( long roleId );
        long[] GetSubSysId ( long[] roleId );
        void Set ( long roleId, RolePower[] powers );
    }
}