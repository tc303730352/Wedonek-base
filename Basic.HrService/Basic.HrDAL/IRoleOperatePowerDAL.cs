using Basic.HrModel.DB;
using Basic.HrModel.OperatePower;

namespace Basic.HrDAL
{
    public interface IRoleOperatePowerDAL : IBasicDAL<DBRoleOperatePower, long>
    {
        long Add ( DBRoleOperatePower add );
        void Clear ( long roleId );
        void Set ( long roleId, long powerId, OperateItem[] powers );
    }
}